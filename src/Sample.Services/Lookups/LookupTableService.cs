using Sample.Core;
using Sample.Core.Defaults;
using Sample.Core.Domain.Lookups;
using Sample.Core.Repository.Lookups;
using Sample.Services.ExportImport;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Services.Lookups
{
    public partial class LookupTableService : BaseService, ILookupTableService
    {
        #region Fields

        private readonly ILookupTableRepository _lookupTableRepository;
        private readonly IExportManager<LookupTableGrid, LookupTable> _exportManager;

        #endregion Fields

        #region Constructor

        public LookupTableService(IUnitOfWork unitOfWork, ILookupTableRepository lookupTableRepository, IExportManager<LookupTableGrid, LookupTable> exportManager)
            : base(unitOfWork)
        {
            _lookupTableRepository = lookupTableRepository;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual List<LookupTable> GetAllLookupTables()
        {
            List<LookupTable> LoadLookupTablesFunc()
            {
                var query = from s in _lookupTableRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadLookupTablesFunc();
        }

        public virtual LookupTable GetLookupTableById(long lookupTableId)
        {
            if (lookupTableId == 0)
                return null;

            LookupTable LoadLookupTableFunc()
            {
                return _lookupTableRepository.GetById(lookupTableId);
            }

            return LoadLookupTableFunc();
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<LookupTable> SearchLookupTables(LookupTableSearch lookupTableSearch)
        {
            return _lookupTableRepository.SearchLookupTables(lookupTableSearch);
        }

        public string ExportLookupTables(LookupTableSearch lookupTableSearch)
        {
            var list = (List<LookupTable>)_lookupTableRepository.SearchAllLookupTables(lookupTableSearch);

            return _exportManager.ExportToExcel(list);
        }

        public LookupTable GetByTypeName(EnumClasses.LookupTypes lookupType, string name)
        {
            return _lookupTableRepository.GetByTypeName(lookupType, name);
        }

        public List<SelectListItem> GetByType(EnumClasses.LookupTypes lookupType)
        {
            var selectListItems = new List<SelectListItem>();
            var lookupTables = _lookupTableRepository.GetByType(lookupType);

            foreach (var item in lookupTables)
            {
                selectListItems.Add(new SelectListItem() { Text = item.Description, Value = item.Id.ToString() });
            }

            return selectListItems;
        }

        #endregion Methods
    }
}