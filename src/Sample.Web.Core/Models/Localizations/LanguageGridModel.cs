using Sample.Core.Defaults;
using System.ComponentModel;

namespace Sample.Web.Core.Models.Localizations
{
    public partial class LanguageGridModel : BaseGridModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LanguageGridModel_Name_DisplayName)]
        public string Name { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LanguageGridModel_Culture_DisplayName)]
        public string Culture { get; set; }

        #endregion Properties
    }
}