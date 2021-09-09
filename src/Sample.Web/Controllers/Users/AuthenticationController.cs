using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Sample.Core.Defaults;
using Sample.Core.Domain.Users;
using Sample.Services.Users;
using Sample.Web.Core.Models;
using Sample.Web.Core.Models.Users;
using Sample.Web.Controllers;

namespace Sample.Web.Controllers.Users
{
    public class AuthenticationController : BaseAuthenticationController
    {
        #region Fields

        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _config;

        #endregion Fields

        #region Constructor

        public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper, IMemoryCache memoryCache, IConfiguration config)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _config = config;
        }

        #endregion Constructor

        #region Base Methods

        [HttpPost("Login")]
        [AllowAnonymous]
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

                var tokenString = GenerateJSONWebToken(value);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = tokenString };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        [HttpPost("Logout")]
        [AllowAnonymous]
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

        private string GenerateJSONWebToken(UserModel userModel)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userModel.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion Base Methods
    }
}