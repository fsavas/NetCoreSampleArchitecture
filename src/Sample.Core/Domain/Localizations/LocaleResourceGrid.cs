using Sample.Core.Defaults;
using System.ComponentModel;

namespace Sample.Core.Domain.Localizations
{
    public partial class LocaleResourceGrid : BaseGrid
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LocaleResourceGridModel_Name_DisplayName)]
        public string Name { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LocaleResourceGridModel_Value_DisplayName)]
        public string Value { get; set; }

        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LocaleResourceGridModel_Language_Name_DisplayName)]
        public string Language_Name { get; set; }

        #endregion Properties
    }
}