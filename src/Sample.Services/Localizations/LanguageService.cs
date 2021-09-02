using Microsoft.Extensions.Caching.Memory;
using Sample.Core;
using Sample.Core.Defaults;
using Sample.Core.Domain.Localizations;
using Sample.Core.Repository.Localizations;
using Sample.Core.Repository.Lookups;
using Sample.Services.ExportImport;
using System;
using System.Collections.Generic;

namespace Sample.Services.Localizations
{
    public partial class LanguageService : BaseService, ILanguageService
    {
        #region Fields

        private readonly ILanguageRepository _languageRepository;
        private readonly ILocaleResourceRepository _localeResourceRepository;
        private readonly ILookupTableRepository _lookupTableRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IExportManager<LanguageGrid, Language> _exportManager;

        #endregion Fields

        #region Constructor

        public LanguageService(IUnitOfWork unitOfWork, ILanguageRepository languageRepository, ILocaleResourceRepository localeResourceRepository, ILookupTableRepository lookupTableRepository, IMemoryCache memoryCache, IExportManager<LanguageGrid, Language> exportManager)
            : base(unitOfWork)
        {
            _languageRepository = languageRepository;
            _localeResourceRepository = localeResourceRepository;
            _lookupTableRepository = lookupTableRepository;
            _memoryCache = memoryCache;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeleteLanguage(long languageId)
        {
            var language = GetLanguageById(languageId);

            if (language == null)
                throw new ArgumentNullException(nameof(language));

            language.IsDeleted = true;
            _languageRepository.Update(language);
            _unitOfWork.SaveChanges();
        }

        public virtual List<Language> GetAllLanguages()
        {
            List<Language> LoadLanguagesFunc()
            {
                //var query = from s in _languageRepository.Table orderby s.Id select s;
                //return query.ToList();
                return _languageRepository.GetAllLanguages();
            }

            return LoadLanguagesFunc();
        }

        public virtual Language GetLanguageById(long languageId)
        {
            if (languageId == 0)
                return null;

            Language LoadLanguageFunc()
            {
                return _languageRepository.GetById(languageId);
            }

            return LoadLanguageFunc();
        }

        public virtual void InsertLanguage(Language language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            _languageRepository.Insert(language);
            _unitOfWork.SaveChanges();
        }

        public virtual void UpdateLanguage(Language language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            _languageRepository.Update(language);
            _unitOfWork.SaveChanges();
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<Language> SearchLanguages(LanguageSearch languageSearch)
        {
            return _languageRepository.SearchLanguages(languageSearch);
        }

        public string ExportLanguages(LanguageSearch languageSearch)
        {
            var list = (List<Language>)_languageRepository.SearchAllLanguages(languageSearch);

            return _exportManager.ExportToExcel(list);
        }

        public bool SelectLanguage(long languageId)
        {
            var language = GetLanguageById(languageId);

            if (language != null)
            {
                var cacheOptions = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal);
                _memoryCache.Set(MemoryCacheKeys.Language, language, cacheOptions);

                var lookupTableLocaleResources = _localeResourceRepository.GetByNamePrefixLanguage(nameof(EnumClasses), languageId);

                foreach (var localeResource in lookupTableLocaleResources)
                {
                    var lookupTable = _lookupTableRepository.GetByName(localeResource.Name);
                    lookupTable.Description = localeResource.Value;
                    _lookupTableRepository.Update(lookupTable);
                    _unitOfWork.SaveChanges();
                }

                return true;
            }

            return false;
        }

        #endregion Methods
    }
}