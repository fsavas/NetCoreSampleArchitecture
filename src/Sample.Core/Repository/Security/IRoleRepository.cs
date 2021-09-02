using Sample.Core.Domain.Security;
using System.Collections.Generic;

namespace Sample.Core.Repository.Security
{
    public partial interface IRoleRepository : IBaseRepository<Role>
    {
        #region Methods

        IPagedList<Role> SearchRoles(RoleSearch roleSearch);

        List<Role> GetAllRoles();

        IList<Role> SearchAllRoles(RoleSearch roleSearch);

        #endregion Methods
    }
}