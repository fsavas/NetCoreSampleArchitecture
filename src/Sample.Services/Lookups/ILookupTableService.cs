using Sample.Core;
using Sample.Core.Defaults;
using Sample.Core.Domain.Lookups;
using System.Collections.Generic;

namespace Sample.Services.Lookups
{
    public partial interface ILookupTableService : IBaseService
    {
        List<LookupTable> GetAllLookupTables();

        LookupTable GetLookupTableById(long lookupTableId);

        IPagedList<LookupTable> SearchLookupTables(LookupTableSearch lookupTableSearch);

        string ExportLookupTables(LookupTableSearch lookupTableSearch);

        LookupTable GetByTypeName(EnumClasses.LookupTypes lookupType, string name);

        List<SelectListItem> GetByType(EnumClasses.LookupTypes lookupType);
    }
}