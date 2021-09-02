using Sample.Core;
using Sample.Core.Domain.Localizations;
using System.Collections.Generic;

namespace Sample.Services.Localizations
{
    public partial interface ILanguageService : IBaseService
    {
        void DeleteLanguage(long languageId);

        List<Language> GetAllLanguages();

        Language GetLanguageById(long languageId);

        void InsertLanguage(Language language);

        void UpdateLanguage(Language language);

        IPagedList<Language> SearchLanguages(LanguageSearch languageSearch);

        bool SelectLanguage(long languageId);

        string ExportLanguages(LanguageSearch languageSearch);
    }
}