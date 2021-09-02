using Microsoft.EntityFrameworkCore;
using Sample.Core;

namespace Sample.Data
{
    public partial interface IDbContext
    {
        #region Methods

        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        int SaveChanges();

        #endregion Methods
    }
}