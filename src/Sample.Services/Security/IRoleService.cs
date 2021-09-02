using Sample.Core;
using Sample.Core.Domain.Security;
using System.Collections.Generic;

namespace Sample.Services.Roles
{
    public partial interface IRoleService : IBaseService
    {
        void DeleteRole(long roleId);

        List<Role> GetAllRoles();

        Role GetRoleById(long roleId);

        void InsertRole(Role role);

        void UpdateRole(Role role);

        IPagedList<Role> SearchRoles(RoleSearch roleSearch);

        string ExportRoles(RoleSearch roleSearch);
    }
}