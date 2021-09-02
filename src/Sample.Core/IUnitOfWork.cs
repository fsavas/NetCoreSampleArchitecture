using Sample.Core.Repository;

namespace Sample.Core
{
    public interface IUnitOfWork
    {
        #region Methods

        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;

        int SaveChanges(bool isTask = false);

        #endregion Methods
    }
}