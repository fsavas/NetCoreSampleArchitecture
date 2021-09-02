using Sample.Core.Defaults;
using System.ComponentModel;

namespace Sample.Web.Core.Models.Localizations
{
    public partial class LocaleResourceSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LocaleResourceSearchModel_Name_DisplayName)]
        public string Name { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LocaleResourceSearchModel_Value_DisplayName)]
        public string Value { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LocaleResourceSearchModel_LanguageId_DisplayName)]
        public long LanguageId { get; set; }

        #endregion Properties
    }
}