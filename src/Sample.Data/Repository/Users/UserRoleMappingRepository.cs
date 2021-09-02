using Sample.Core.Domain.Users;
using Sample.Core.Repository.Users;

namespace Sample.Data.Repository.Users
{
    public class UserRoleMappingRepository : BaseRepository<UserRoleMapping>, IUserRoleMappingRepository
    {
        #region Constructor

        public UserRoleMappingRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor
    }
}