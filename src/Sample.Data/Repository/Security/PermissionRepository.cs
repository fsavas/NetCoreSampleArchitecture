using Sample.Core;
using Sample.Core.Domain.Security;
using Sample.Core.Helpers;
using Sample.Core.Repository.Security;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Data.Repository.Security
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        #region Fields

        private readonly IRolePermissionMappingRepository _rolePermissionMappingRepository;

        #endregion Fields

        #region Constructor

        public PermissionRepository(IDbContext context, IRolePermissionMappingRepository rolePermissionMappingRepository)
            : base(context)
        {
            _rolePermissionMappingRepository = rolePermissionMappingRepository;
        }

        #endregion Constructor

        #region Methods

        public IPagedList<Permission> SearchPermissions(PermissionSearch permissionSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, permissionSearch);

            return new PagedList<Permission>(query, permissionSearch.Page - 1, permissionSearch.PageSize);
        }

        public IList<Permission> SearchAllPermissions(PermissionSearch permissionSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, permissionSearch);

            return query.ToList();
        }

        public List<Permission> GetAllPermissions()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        private IQueryable<Permission> AddQueryCriteria(IQueryable<Permission> query, PermissionSearch permissionSearch)
        {
            if (!string.IsNullOrEmpty(permissionSearch.Name))
                query = query.Where(s => s.Name.Contains(permissionSearch.Name));
            if (permissionSearch.RoleId > 0)
                query = query.Join(_rolePermissionMappingRepository.Table, x => x.Id, y => y.PermissionId,
                    (x, y) => new { Permission = x, Mapping = y })
                    .Where(z => z.Mapping.RoleId == permissionSearch.RoleId)
                    .Select(z => z.Permission);

            return LinqHelper<Permission>.OrderBy(query, permissionSearch.OrderMember, permissionSearch.OrderByAsc);
        }

        #endregion Methods
    }
}