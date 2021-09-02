using Sample.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample.Web.Core.Models.Security
{
    public partial class PermissionModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.Sample_Web_Core_Models_Security_PermissionModel_Code_Required)]
        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Security_PermissionModel_Code_DisplayName)]
        public string Code { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.Sample_Web_Core_Models_Security_PermissionModel_Name_Required)]
        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Security_PermissionModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}