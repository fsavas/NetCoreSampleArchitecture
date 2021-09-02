using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Sample.Core;
using Sample.Core.Defaults;
using Sample.Core.Domain.Lookups;
using Sample.Services.Lookups;
using Sample.Web.Core.Models;
using Sample.Web.Core.Models.Lookups;
using System;

namespace Sample.Web.Controllers.Lookups
{
    public class LookupTableController : BaseController
    {
        #region Fields

        private readonly ILookupTableService _lookupTableService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public LookupTableController(ILookupTableService lookupTableService, IMapper mapper, IMemoryCache memoryCache)
        {
            _lookupTableService = lookupTableService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/LookupTable
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] LookupTableSearchModel value)
        {
            try
            {
                var lookupTableSearch = _mapper.Map<LookupTableSearch>(value);
                var lookupTablePagedList = (PagedList<LookupTable>)_lookupTableService.SearchLookupTables(lookupTableSearch);
                var data = _mapper.Map<PagedList<LookupTable>, PagedList<LookupTableGridModel>>(lookupTablePagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/LookupTable
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] LookupTableSearchModel value)
        {
            try
            {
                var lookupTableSearch = _mapper.Map<LookupTableSearch>(value);
                var data = _lookupTableService.ExportLookupTables(lookupTableSearch);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/LookupTable/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var lookupTable = _lookupTableService.GetLookupTableById(id);
                var data = _mapper.Map<LookupTableModel>(lookupTable);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        #endregion Base Methods
    }
}