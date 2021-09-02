using Sample.Core.Domain.Security;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Core.Domain.Users
{
    public partial class User : BaseEntity
    {
        private ICollection<UserRoleMapping> _userRoleMappings;
        private IList<Role> _roles;

        public string Username { get; set; }
        public byte[] Key { get; set; }
        public byte[] Salt { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }

        #region Properties

        public virtual IList<Role> Roles
        {
            get => _roles ?? (_roles = UserRoleMappings.Select(mapping => mapping.Role).ToList());
        }

        public virtual ICollection<UserRoleMapping> UserRoleMappings
        {
            get => _userRoleMappings ?? (_userRoleMappings = new List<UserRoleMapping>());
            protected set => _userRoleMappings = value;
        }

        #endregion Properties

        #region Methods

        public void AddUserRoleMapping(UserRoleMapping role)
        {
            UserRoleMappings.Add(role);
            _roles = null;
        }

        public void RemoveUserRoleMapping(UserRoleMapping role)
        {
            UserRoleMappings.Remove(role);
            _roles = null;
        }

        #endregion Methods
    }
}