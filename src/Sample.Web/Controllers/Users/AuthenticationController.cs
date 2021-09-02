using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Sample.Core.Defaults;
using Sample.Core.Domain.Users;
using Sample.Services.Users;
using Sample.Web.Core.Models;
using Sample.Web.Core.Models.Users;
using System;

namespace Sample.Web.Controllers.Users
{
    public class AuthenticationController : BaseAuthenticationController
    {
        #region Fields

        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper, IMemoryCache memoryCache)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        [HttpPost("Login")]
        public ServiceResult PostLogin([FromBody] UserModel value)
        {
            try
            {
                var user = _mapper.Map<User>(value);

                var data = _authenticationService.Login(user);

                if (!data)
                {
                    if (_memoryCache.TryGetValue(MemoryCacheKeys.InvalidUsernamePassword, out string error))
                        return new ServiceResult { Success = false, Message = error, Data = null };

                    return new ServiceResult { Success = false, Message = MemoryCacheKeys.InvalidUsernamePassword, Data = null };
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

        [HttpPost("Logout")]
        public ServiceResult PostLogout()
        {
            try
            {
                var data = _authenticationService.Logout();

                if (!data)
                {
                    if (_memoryCache.TryGetValue(MemoryCacheKeys.LogoutUnsuccessful, out string error))
                        return new ServiceResult { Success = false, Message = error, Data = null };

                    return new ServiceResult { Success = false, Message = MemoryCacheKeys.LogoutUnsuccessful, Data = null };
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

        #endregion Base Methods
    }
}