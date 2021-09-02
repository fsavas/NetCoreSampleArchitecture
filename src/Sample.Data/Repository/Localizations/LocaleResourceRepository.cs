using Sample.Core;
using Sample.Core.Domain.Localizations;
using Sample.Core.Helpers;
using Sample.Core.Repository.Localizations;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Data.Repository.Localizations
{
    public class LocaleResourceRepository : BaseRepository<LocaleResource>, ILocaleResourceRepository
    {
        #region Constructor

        public LocaleResourceRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor

        #region Methods

        public IPagedList<LocaleResource> SearchLocaleResources(LocaleResourceSearch localeResourceSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, localeResourceSearch);

            return new PagedList<LocaleResource>(query, localeResourceSearch.Page - 1, localeResourceSearch.PageSize);
        }

        public IList<LocaleResource> SearchAllLocaleResources(LocaleResourceSearch localeResourceSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, localeResourceSearch);

            return query.ToList();
        }

        public List<LocaleResource> GetAllLocaleResources()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        public List<LocaleResource> GetByLanguage(long languageId)
        {
            var query = from s in Table where s.LanguageId == languageId select s;

            return query.ToList();
        }

        public List<LocaleResource> GetByNamePrefixLanguage(string name, long languageId)
        {
            var query = Table;

            if (!string.IsNullOrEmpty(name) && languageId > 0)
            {
                query = query.Where(s => s.Name.StartsWith(name) && s.LanguageId == languageId);

                return query.ToList();
            }

            return new List<LocaleResource>();
        }

        private IQueryable<LocaleResource> AddQueryCriteria(IQueryable<LocaleResource> query, LocaleResourceSearch localeResourceSearch)
        {
            if (!string.IsNullOrEmpty(localeResourceSearch.Name))
                query = query.Where(s => s.Name.Contains(localeResourceSearch.Name));
            if (!string.IsNullOrEmpty(localeResourceSearch.Value))
                query = query.Where(s => s.Value.Contains(localeResourceSearch.Value));
            if (localeResourceSearch.LanguageId > 0)
                query = query.Where(s => s.LanguageId == localeResourceSearch.LanguageId);

            return LinqHelper<LocaleResource>.OrderBy(query, localeResourceSearch.OrderMember, localeResourceSearch.OrderByAsc);
        }

        #endregion Methods
    }
}