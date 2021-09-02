using Sample.Core.Defaults;
using System.ComponentModel;

namespace Sample.Web.Core.Models.Localizations
{
    public partial class LanguageSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LanguageSearchModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}