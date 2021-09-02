using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Sample.Core;
using Sample.Core.Defaults;
using Sample.Core.Domain.Localizations;
using Sample.Services.Localizations;
using Sample.Web.Core.Models;
using Sample.Web.Core.Models.Localizations;
using System;

namespace Sample.Web.Controllers.Localization
{
    public class LocaleResourceController : BaseController
    {
        #region Fields

        private readonly ILocaleResourceService _localeResourceService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public LocaleResourceController(ILocaleResourceService localeResourceService, IMapper mapper, IMemoryCache memoryCache)
        {
            _localeResourceService = localeResourceService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/LocaleResource
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] LocaleResourceSearchModel value)
        {
            try
            {
                var localeResourceSearch = _mapper.Map<LocaleResourceSearch>(value);
                var localeResourcePagedList = (PagedList<LocaleResource>)_localeResourceService.SearchLocaleResources(localeResourceSearch);
                var data = _mapper.Map<PagedList<LocaleResource>, PagedList<LocaleResourceGridModel>>(localeResourcePagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/LocaleResource
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] LocaleResourceSearchModel value)
        {
            try
            {
                var localeResourceSearch = _mapper.Map<LocaleResourceSearch>(value);
                var data = _localeResourceService.ExportLocaleResources(localeResourceSearch);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Language
        [HttpPost("New")]
        public ServiceResult PostNew()
        {
            try
            {
                var data = InitializeLocaleResource();

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/LocaleResource/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var localeResource = _localeResourceService.GetLocaleResourceById(id);
                var data = _mapper.Map<LocaleResourceModel>(localeResource);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/LocaleResource
        [HttpPost]
        public ServiceResult Post([FromBody] LocaleResourceModel value)
        {
            try
            {
                var localeResource = _mapper.Map<LocaleResource>(value);

                if (localeResource.Id > 0)
                    _localeResourceService.UpdateLocaleResource(localeResource);
                else
                    _localeResourceService.InsertLocaleResource(localeResource);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // DELETE: api/LocaleResource/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(long id)
        {
            try
            {
                _localeResourceService.DeleteLocaleResource(id);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        #endregion Base Methods

        #region Methods

        private LocaleResourceModel InitializeLocaleResource()
        {
            return new LocaleResourceModel();
        }

        #endregion Methods
    }
}