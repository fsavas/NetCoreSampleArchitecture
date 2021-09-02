using Sample.Core.Domain.Security;

namespace Sample.Core.Domain.Users
{
    public partial class UserRoleMapping : BaseEntity
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}