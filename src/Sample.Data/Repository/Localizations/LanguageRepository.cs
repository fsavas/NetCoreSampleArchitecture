using Sample.Core;
using Sample.Core.Domain.Localizations;
using Sample.Core.Helpers;
using Sample.Core.Repository.Localizations;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Data.Repository.Localizations
{
    public class LanguageRepository : BaseRepository<Language>, ILanguageRepository
    {
        #region Constructor

        public LanguageRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor

        #region Methods

        public IPagedList<Language> SearchLanguages(LanguageSearch languageSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, languageSearch);

            return new PagedList<Language>(query, languageSearch.Page - 1, languageSearch.PageSize);
        }

        public IList<Language> SearchAllLanguages(LanguageSearch languageSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, languageSearch);

            return query.ToList();
        }

        public List<Language> GetAllLanguages()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        private IQueryable<Language> AddQueryCriteria(IQueryable<Language> query, LanguageSearch languageSearch)
        {
            if (!string.IsNullOrEmpty(languageSearch.Name))
                query = query.Where(s => s.Name.Contains(languageSearch.Name));

            return LinqHelper<Language>.OrderBy(query, languageSearch.OrderMember, languageSearch.OrderByAsc);
        }

        #endregion Methods
    }
}