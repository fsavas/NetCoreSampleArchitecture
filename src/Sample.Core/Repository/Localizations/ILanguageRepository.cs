using Sample.Core.Domain.Localizations;
using System.Collections.Generic;

namespace Sample.Core.Repository.Localizations
{
    public partial interface ILanguageRepository : IBaseRepository<Language>
    {
        #region Methods

        IPagedList<Language> SearchLanguages(LanguageSearch languageSearch);

        List<Language> GetAllLanguages();

        IList<Language> SearchAllLanguages(LanguageSearch languageSearch);

        #endregion Methods
    }
}