namespace Sample.Core.Domain.Security
{
    public partial class RolePermissionMapping : BaseEntity
    {
        public long PermissionId { get; set; }
        public long RoleId { get; set; }
        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}