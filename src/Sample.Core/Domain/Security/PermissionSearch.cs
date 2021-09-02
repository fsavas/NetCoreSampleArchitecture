namespace Sample.Core.Domain.Security
{
    public partial class PermissionSearch : BaseSearch
    {
        #region Properties

        public string Name { get; set; }
        public long RoleId { get; set; }

        #endregion Properties
    }
}