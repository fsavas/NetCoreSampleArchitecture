using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Sample.Core;
using Sample.Core.Defaults;
using Sample.Core.Domain.Security;
using Sample.Core.Domain.Users;
using Sample.Services.Roles;
using Sample.Services.Users;
using Sample.Web.Core.Models;
using Sample.Web.Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Web.Controllers.Users
{
    public class UserController : BaseController
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public UserController(IUserService userService, IRoleService roleService, IMapper mapper, IMemoryCache memoryCache)
        {
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/User
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] UserSearchModel value)
        {
            try
            {
                var userSearch = _mapper.Map<UserSearch>(value);
                var userPagedList = (PagedList<User>)_userService.SearchUsers(userSearch);
                var data = _mapper.Map<PagedList<User>, PagedList<UserGridModel>>(userPagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/User
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] UserSearchModel value)
        {
            try
            {
                var userSearch = _mapper.Map<UserSearch>(value);
                var data = _userService.ExportUsers(userSearch);

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
                var data = InitializeUser();

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/User/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                var data = _mapper.Map<UserModel>(user);

                GetUserRoleMapping(user, data);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/User
        [HttpPost]
        public ServiceResult Post([FromBody] UserModel value)
        {
            try
            {
                var user = _mapper.Map<User>(value);

                AddUserRoleMapping(user, value);

                if (user.Id > 0)
                    _userService.UpdateUser(user);
                else
                    _userService.InsertUser(user);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(long id)
        {
            try
            {
                _userService.DeleteUser(id);

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

        private void AddUserRoleMapping(User user, UserModel model)
        {
            var allRoles = _roleService.GetAllRoles();
            var userRoles = new List<Role>();

            foreach (var role in allRoles)
                if (model.SelectedRoles.Where(x => x.Value == role.Id.ToString()).Any())
                    userRoles.Add(role);

            foreach (var userRole in userRoles)
            {
                user.AddUserRoleMapping(new UserRoleMapping { Role = userRole });
            }
        }

        private void GetUserRoleMapping(User user, UserModel model)
        {
            var userRoles = new List<SelectListItemModel>();

            foreach (var userRoleMapping in user.UserRoleMappings)
            {
                var role = userRoleMapping.Role;
                userRoles.Add(new SelectListItemModel() { Text = role.Name, Value = role.Id.ToString() });
            }

            model.SelectedRoles = userRoles;
        }

        private List<SelectListItemModel> GetAvailableRoles()
        {
            var availableRoles = new List<SelectListItemModel>();
            var allRoles = _roleService.GetAllRoles();

            foreach (var role in allRoles)
            {
                availableRoles.Add(new SelectListItemModel() { Text = role.Name, Value = role.Id.ToString() });
            }

            return availableRoles;
        }

        private UserModel InitializeUser()
        {
            return new UserModel() { AvailableRoles = GetAvailableRoles() };
        }

        #endregion Methods
    }
}