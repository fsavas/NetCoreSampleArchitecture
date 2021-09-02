using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Sample.Core;
using Sample.Core.Defaults;
using Sample.Core.Domain.Security;
using Sample.Services.Permissions;
using Sample.Web.Core.Models;
using Sample.Web.Core.Models.Security;
using System;

namespace Sample.Web.Controllers.Security
{
    public class PermissionController : BaseController
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public PermissionController(IPermissionService permissionService, IMapper mapper, IMemoryCache memoryCache)
        {
            _permissionService = permissionService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/Permission
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] PermissionSearchModel value)
        {
            try
            {
                var permissionSearch = _mapper.Map<PermissionSearch>(value);
                var permissionPagedList = (PagedList<Permission>)_permissionService.SearchPermissions(permissionSearch);
                var data = _mapper.Map<PagedList<Permission>, PagedList<PermissionGridModel>>(permissionPagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Permission
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] PermissionSearchModel value)
        {
            try
            {
                var permissionSearch = _mapper.Map<PermissionSearch>(value);
                var data = _permissionService.ExportPermissions(permissionSearch);

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
                var data = InitializePermission();

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/Permission/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var permission = _permissionService.GetPermissionById(id);
                var data = _mapper.Map<PermissionModel>(permission);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Permission
        [HttpPost]
        public ServiceResult Post([FromBody] PermissionModel value)
        {
            try
            {
                var permission = _mapper.Map<Permission>(value);

                if (permission.Id > 0)
                    _permissionService.UpdatePermission(permission);
                else
                    _permissionService.InsertPermission(permission);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // DELETE: api/Permission/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(long id)
        {
            try
            {
                _permissionService.DeletePermission(id);

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

        // POST: api/Permission
        [HttpPost("HavePermission")]
        public ServiceResult PostHavePermission([FromBody] string value)
        {
            try
            {
                var data = _permissionService.HavePermission(value);

                if (!data)
                {
                    if (_memoryCache.TryGetValue(MemoryCacheKeys.PermissionDenied, out string error))
                        return new ServiceResult { Success = false, Message = error, Data = null };

                    return new ServiceResult { Success = false, Message = MemoryCacheKeys.PermissionDenied, Data = null };
                }

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Permission
        [HttpPost("PermissionsByPrefix")]
        public ServiceResult PostPermissionsByPrefix([FromBody] string value)
        {
            try
            {
                var data = _permissionService.GetPermissionsByPrefix(value);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        #endregion Methods

        #region Methods

        private PermissionModel InitializePermission()
        {
            return new PermissionModel();
        }

        #endregion Methods
    }
}