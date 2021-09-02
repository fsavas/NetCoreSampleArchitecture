using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Sample.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSerilogServices(this IServiceCollection services, ILogger logger)
        {
            return services.AddSingleton(logger);
        }

        //public static IServiceCollection AddAutoMapperServices(this IServiceCollection services, AppDomainTypeFinder typeFinder)
        //{
        //    //find mapper configurations
        //    var mapperConfigurations = typeFinder.FindClassesOfType<Profile>();

        //    //create instances of mapper configurations
        //    var instances = mapperConfigurations
        //        .Select(mapperConfiguration => (Profile)Activator.CreateInstance(mapperConfiguration));

        //    //create AutoMapper configuration
        //    var mappingConfig = new MapperConfiguration(cfg =>
        //    {
        //        foreach (var instance in instances)
        //        {
        //            cfg.AddProfile(instance.GetType());
        //        }
        //    });

        //    //register
        //    IMapper mapper = mappingConfig.CreateMapper();
        //    services.AddSingleton(mapper);

        //    return services;
        //}
    }
}