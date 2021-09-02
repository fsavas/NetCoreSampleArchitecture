using Sample.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample.Web.Core.Models.Localizations
{
    public partial class LanguageModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LanguageModel_Name_Required)]
        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LanguageModel_Name_DisplayName)]
        public string Name { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LanguageModel_Culture_Required)]
        [DisplayName(MemoryCacheKeys.Sample_Web_Core_Models_Localizations_LanguageModel_Culture_DisplayName)]
        public string Culture { get; set; }

        #endregion Properties
    }
}