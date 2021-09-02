using Sample.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample.Web.Core.Models.Localizations
{
    public partial class LocaleResourceModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LocaleResourceModel_Name_Required)]
        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LocaleResourceModel_Name_DisplayName)]
        public string Name { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LocaleResourceModel_Value_Required)]
        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LocaleResourceModel_Value_DisplayName)]
        public string Value { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LocaleResourceModel_LanguageId_Required)]
        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LocaleResourceModel_LanguageId_DisplayName)]
        public long? LanguageId { get; set; }

        #endregion Properties
    }
}