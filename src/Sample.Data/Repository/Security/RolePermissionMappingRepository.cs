using Sample.Core.Domain.Security;
using Sample.Core.Repository.Security;

namespace Sample.Data.Repository.Security
{
    public class RolePermissionMappingRepository : BaseRepository<RolePermissionMapping>, IRolePermissionMappingRepository
    {
        #region Constructor

        public RolePermissionMappingRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor
    }
}