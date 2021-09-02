using Sample.Core.Defaults;
using System.ComponentModel;

namespace Sample.Web.Core.Models.Security
{
    public partial class RoleSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Security_RoleSearchModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}