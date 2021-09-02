using System.Collections.Generic;
using System.Linq;

namespace Sample.Core.Repository
{
    public partial interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Methods

        TEntity GetById(object id);

        void Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        void InsertOrUpdate(TEntity entity);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        #endregion Methods

        #region Properties

        IQueryable<TEntity> Table { get; }

        #endregion Properties
    }
}