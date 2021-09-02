using Microsoft.EntityFrameworkCore;
using Sample.Core;
using Sample.Data.Mapping;
using System;
using System.Linq;
using System.Reflection;

namespace Sample.Data
{
    public partial class BaseObjectContext : DbContext, IDbContext
    {
        #region Constructor

        public BaseObjectContext(DbContextOptions<BaseObjectContext> options) : base(options)
        {
        }

        #endregion Constructor

        #region Methods

        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        #endregion Methods

        #region Utilities

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //dynamically load all entity and query type configurations
            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                (type.BaseType?.IsGenericType ?? false)
                    && (type.BaseType.GetGenericTypeDefinition() == typeof(BaseEntityTypeConfiguration<>)));

            foreach (var typeConfiguration in typeConfigurations)
            {
                var configuration = (IMappingConfiguration)Activator.CreateInstance(typeConfiguration);
                configuration.ApplyConfiguration(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);
        }

        #endregion Utilities
    }
}