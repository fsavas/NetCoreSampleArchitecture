using Sample.Core.Defaults;
using System.ComponentModel;

namespace Sample.Core.Domain.Security
{
    public partial class RoleGrid : BaseGrid
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Security_RoleGridModel_Code_DisplayName)]
        public string Code { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Security_RoleGridModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}