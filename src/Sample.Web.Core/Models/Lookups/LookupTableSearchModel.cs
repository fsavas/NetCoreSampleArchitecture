using Sample.Core.Defaults;
using System.ComponentModel;

namespace Sample.Web.Core.Models.Lookups
{
    public partial class LookupTableSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Lookups_LookupTableSearchModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}