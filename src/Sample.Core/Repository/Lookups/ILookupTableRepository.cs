using Sample.Core.Defaults;
using Sample.Core.Domain.Lookups;
using System.Collections.Generic;

namespace Sample.Core.Repository.Lookups
{
    public partial interface ILookupTableRepository : IBaseRepository<LookupTable>
    {
        #region Methods

        IPagedList<LookupTable> SearchLookupTables(LookupTableSearch lookupTableSearch);

        List<LookupTable> GetAllLookupTables();

        IList<LookupTable> SearchAllLookupTables(LookupTableSearch lookupTableSearch);

        LookupTable GetByTypeName(EnumClasses.LookupTypes lookupType, string name);

        List<LookupTable> GetByType(EnumClasses.LookupTypes lookupType);

        LookupTable GetByName(string name);

        #endregion Methods
    }
}