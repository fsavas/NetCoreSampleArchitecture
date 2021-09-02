using Sample.Core.Defaults;
using System.ComponentModel;

namespace Sample.Web.Core.Models.Security
{
    public partial class PermissionSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Security_PermissionSearchModel_Name_DisplayName)]
        public string Name { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Security_PermissionSearchModel_RoleId_DisplayName)]
        public long RoleId { get; set; }

        #endregion Properties
    }
}