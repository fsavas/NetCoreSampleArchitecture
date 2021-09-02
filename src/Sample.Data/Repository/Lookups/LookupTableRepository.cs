using Sample.Core;
using Sample.Core.Defaults;
using Sample.Core.Domain.Lookups;
using Sample.Core.Helpers;
using Sample.Core.Repository.Lookups;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Data.Repository.Lookups
{
    public class LookupTableRepository : BaseRepository<LookupTable>, ILookupTableRepository
    {
        #region Constructor

        public LookupTableRepository(IDbContext context)
            : base(context)
        {
        }

        #endregion Constructor

        #region Methods

        public IPagedList<LookupTable> SearchLookupTables(LookupTableSearch lookupTableSearch)
        {
            var query = Table;
            AddQueryCriteria(query, lookupTableSearch);

            return new PagedList<LookupTable>(query, lookupTableSearch.Page - 1, lookupTableSearch.PageSize);
        }

        public IList<LookupTable> SearchAllLookupTables(LookupTableSearch lookupTableSearch)
        {
            var query = Table;
            query = AddQueryCriteria(query, lookupTableSearch);

            return query.ToList();
        }

        public List<LookupTable> GetAllLookupTables()
        {
            var query = from s in Table orderby s.Id select s;

            return query.ToList();
        }

        private IQueryable<LookupTable> AddQueryCriteria(IQueryable<LookupTable> query, LookupTableSearch lookupTableSearch)
        {
            if (!string.IsNullOrEmpty(lookupTableSearch.Name))
                query = query.Where(s => s.Name.Contains(lookupTableSearch.Name));

            return LinqHelper<LookupTable>.OrderBy(query, lookupTableSearch.OrderMember, lookupTableSearch.OrderByAsc);
        }

        public LookupTable GetByTypeName(EnumClasses.LookupTypes lookupType, string name)
        {
            var query = Table;
            query = query.Where(s => s.LookupType == lookupType && s.Name == name);

            return query.SingleOrDefault();
        }

        public List<LookupTable> GetByType(EnumClasses.LookupTypes lookupType)
        {
            var query = Table;
            query = query.Where(s => s.LookupType == lookupType);

            return query.ToList();
        }

        public LookupTable GetByName(string name)
        {
            var query = Table;
            query = query.Where(s => s.Name == name);

            return query.SingleOrDefault();
        }

        #endregion Methods
    }
}