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
    public class LanguageController : BaseController
    {
        #region Fields

        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public LanguageController(ILanguageService languageService, IMapper mapper, IMemoryCache memoryCache)
        {
            _languageService = languageService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/Language
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] LanguageSearchModel value)
        {
            try
            {
                var languageSearch = _mapper.Map<LanguageSearch>(value);
                var languagePagedList = (PagedList<Language>)_languageService.SearchLanguages(languageSearch);
                var data = _mapper.Map<PagedList<Language>, PagedList<LanguageGridModel>>(languagePagedList);

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
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] LanguageSearchModel value)
        {
            try
            {
                var languageSearch = _mapper.Map<LanguageSearch>(value);
                var data = _languageService.ExportLanguages(languageSearch);

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
                var data = InitializeLanguage();

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
        [HttpPost("Select")]
        public ServiceResult PostSelect([FromBody] long id)
        {
            try
            {
                bool result = _languageService.SelectLanguage(id);

                if (result)
                {
                    if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                        successMessage = message;

                    return new ServiceResult { Success = true, Message = successMessage, Data = null };
                }

                return new ServiceResult { Success = false, Message = "Dil değişikliği yapılamadı", Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/Language/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var language = _languageService.GetLanguageById(id);
                var data = _mapper.Map<LanguageModel>(language);

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
        [HttpPost]
        public ServiceResult Post([FromBody] LanguageModel value)
        {
            try
            {
                var language = _mapper.Map<Language>(value);

                if (language.Id > 0)
                    _languageService.UpdateLanguage(language);
                else
                    _languageService.InsertLanguage(language);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // DELETE: api/Language/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(long id)
        {
            try
            {
                _languageService.DeleteLanguage(id);

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

        private LanguageModel InitializeLanguage()
        {
            return new LanguageModel();
        }

        #endregion Methods
    }
}