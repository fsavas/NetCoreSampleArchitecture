using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sample.Core;
using Sample.Core.Encryption;
using Sample.Core.Extensions;
using Sample.Core.Infrastructure;
using Sample.Core.Repository;
using Sample.Data;
using Sample.Data.Repository;
using Sample.Services;
using Sample.Services.BackgroundJobs.HostedTasks;
using Sample.Services.BackgroundJobs.ScopedTasks;
using Sample.Services.Enums;
using Sample.Services.Events;
using Sample.Services.ExportImport;
using Sample.Services.Hubs;
using Sample.Services.Rest;
using Sample.Web.Core.Extensions;
using Sample.Web.Mvc.Filters;
using Serilog;
using System;
using System.Linq;
using System.Text;

namespace Sample.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;

            // Read the connection string from the "ASPNETCORE_ConnectionStrings:DefaultConnection" environment variable
            // RESTART Visual Studio if you add or edit environment variables for the change to take effect.
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            // This one needs to be last for the environment variables to have the highest priority
            builder.AddEnvironmentVariables("ASPNETCORE_");
            Configuration = builder.Build();
        }        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var typeFinder = new AppDomainTypeFinder();

            AddCommonServices(services);
            AddDbContextService(services);
            AddDependency(services, typeFinder);
            AddAutoMapper(services, typeFinder);
            //Log is a static class. You dont need to register
            //AddSerilog(services);
            AddEventPublisher(services);
            AddTaskServices(services, typeFinder);
            AddSwaggerService(services);
            AddJwtAuthentication(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //ServiceContainer.Provider = app.ApplicationServices;
            AddCommonApplicationBuilder(app, env);
            AddSwaggerApplicationBuilder(app);
        }

        #region Services

        private void AddCommonServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMemoryCache();
            services.AddMvcCore(options =>
            {
                options.Filters.Add(typeof(ValidateModelFilter));
            });
            services.AddSignalR();
        }

        private void AddDbContextService(IServiceCollection services)
        {
            services.AddDbContextPool<BaseObjectContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DevConnection"),
                options => options.MigrationsAssembly("Sample.Data")).UseLazyLoadingProxies();
            });

            services.AddScoped<IDbContext>(provider => provider.GetService<BaseObjectContext>());
        }

        private void AddDependency(IServiceCollection services, AppDomainTypeFinder typeFinder)
        {
            //unitofwork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services
            services.AddScoped<IBaseService, BaseService>();

            var serviceAssembly = typeFinder.GetAssemblies().FirstOrDefault(a => a.FullName.Contains("Sample.Services"));

            var serviceTypeConfigurations = serviceAssembly.GetTypes().Where(type =>
                (typeof(BaseService).IsAssignableFrom(type))
                    && (type.Name != typeof(BaseService).Name)).OrderBy(x => x.Name).ToList();

            var iServiceTypeConfigurations = serviceAssembly.GetTypes().Where(type =>
                (typeof(IBaseService).IsAssignableFrom(type))
                    && (type.BaseType == null)
                        && (type.Name != typeof(IBaseService).Name)).OrderBy(x => x.Name).ToList();

            foreach (var item in iServiceTypeConfigurations.Zip(serviceTypeConfigurations, Tuple.Create))
            {
                services.AddScoped(item.Item1, item.Item2);
            }

            //repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            var repositoryAssembly = typeFinder.GetAssemblies().FirstOrDefault(a => a.FullName.Contains("Sample.Data"));
            var iRepositoryAssembly = typeFinder.GetAssemblies().FirstOrDefault(a => a.FullName.Contains("Sample.Core"));

            var repositoryTypeConfigurations = repositoryAssembly.GetTypes().Where(type =>
                (type.BaseType?.IsGenericType ?? false)
                    && (type.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<>))).OrderBy(x => x.Name).ToList();

            var iRepositoryTypeConfigurations = iRepositoryAssembly.GetTypes();

            foreach (var typeConfiguration in repositoryTypeConfigurations)
            {
                services.AddScoped(iRepositoryTypeConfigurations.Where(type => type.Name == "I" + typeConfiguration.Name).FirstOrDefault(), typeConfiguration);
            }

            //Encryption
            services.AddScoped<IEncryptionManager, EncryptionManager>();

            //Filters
            services.AddScoped<MemoryCacheFilter>();

            //ExportManager
            services.AddScoped(typeof(IExportManager<,>), typeof(ExportManager<,>));

            //TokenHandler
            services.AddScoped<ITokenHandler, Services.Rest.TokenHandler>();

            //EnumManager
            services.AddScoped<IEnumManager, EnumManager>();
        }

        private void AddAutoMapper(IServiceCollection services, AppDomainTypeFinder typeFinder)
        {
            services.AddAutoMapperServices(typeFinder);

            ////find mapper configurations
            //var mapperConfigurations = typeFinder.FindClassesOfType<Profile>();

            ////create instances of mapper configurations
            //var instances = mapperConfigurations
            //    .Select(mapperConfiguration => (Profile)Activator.CreateInstance(mapperConfiguration));

            ////create AutoMapper configuration
            //var mappingConfig = new MapperConfiguration(cfg =>
            //{
            //    foreach (var instance in instances)
            //    {
            //        cfg.AddProfile(instance.GetType());
            //    }
            //});

            ////register
            //IMapper mapper = mappingConfig.CreateMapper();
            //services.AddSingleton(mapper);
        }

        private void AddSerilog(IServiceCollection services)
        {
            services.AddSerilogServices(Log.Logger);
        }

        private void AddEventPublisher(IServiceCollection services)
        {
            services.AddScoped<IEventPublisher, EventPublisher>();
        }

        private void AddTaskServices(IServiceCollection services, AppDomainTypeFinder typeFinder)
        {
            //var service = services.BuildServiceProvider().GetService<IUserService>();
            //services.AddHostedService<CacheTask>();
            //services.AddSingleton<IHostedService, CacheTask>();
            //var hostedTasks = typeFinder.FindClassesOfType(typeof(BaseBackgroundService));
            //foreach (var hostedTask in hostedTasks)
            //    services.AddSingleton(typeof(IHostedService), hostedTask);

            var scopedTasks = typeFinder.FindClassesOfType(typeof(IScopedProcessingService));

            foreach (var scopedTask in scopedTasks)
                services.AddScoped(typeof(IScopedProcessingService), scopedTask);

            services.AddHostedService<SampleTask>();
        }

        private void AddSwaggerService(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample Service API", Version = "v1" });
            });
        }

        private void AddJwtAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            services.AddMvc();
        }

        #endregion Services

        #region Application

        private void AddCommonApplicationBuilder(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SampleHub>("/sampleHub");
            });
        }

        private void AddSwaggerApplicationBuilder(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
            });
        }

        #endregion Application
    }
}