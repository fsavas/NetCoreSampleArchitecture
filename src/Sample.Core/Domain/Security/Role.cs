using System.Collections.Generic;
using System.Linq;

namespace Sample.Core.Domain.Security
{
    public partial class Role : BaseEntity
    {
        private ICollection<RolePermissionMapping> _rolePermissionMappings;
        private IList<Permission> _permissions;

        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        #region Properties

        public virtual IList<Permission> Permissions
        {
            get => _permissions ?? (_permissions = RolePermissionMappings.Select(mapping => mapping.Permission).ToList());
        }

        public virtual ICollection<RolePermissionMapping> RolePermissionMappings
        {
            get => _rolePermissionMappings ?? (_rolePermissionMappings = new List<RolePermissionMapping>());
            protected set => _rolePermissionMappings = value;
        }

        #endregion Properties

        #region Methods

        public void AddRolePermissionMapping(RolePermissionMapping permission)
        {
            RolePermissionMappings.Add(permission);
            _permissions = null;
        }

        public void RemoveCustomerRoleMapping(RolePermissionMapping permission)
        {
            RolePermissionMappings.Remove(permission);
            _permissions = null;
        }

        #endregion Methods
    }
}