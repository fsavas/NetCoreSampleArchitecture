using Sample.Core;
using Sample.Core.Domain.Security;
using Sample.Core.Helpers;
using Sample.Core.Repository.Security;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Data.Repository.Security
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        #region Constructor

        public RoleRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor

        #region Methods

        public IPagedList<Role> SearchRoles(RoleSearch roleSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, roleSearch);

            return new PagedList<Role>(query, roleSearch.Page - 1, roleSearch.PageSize);
        }

        public IList<Role> SearchAllRoles(RoleSearch roleSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, roleSearch);

            return query.ToList();
        }

        public List<Role> GetAllRoles()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        private IQueryable<Role> AddQueryCriteria(IQueryable<Role> query, RoleSearch roleSearch)
        {
            if (!string.IsNullOrEmpty(roleSearch.Name))
                query = query.Where(s => s.Name.Contains(roleSearch.Name));

            return LinqHelper<Role>.OrderBy(query, roleSearch.OrderMember, roleSearch.OrderByAsc);
        }

        #endregion Methods
    }
}