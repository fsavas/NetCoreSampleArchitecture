using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Sample.Core.Defaults;
using Sample.Core.Domain.Users;
using Sample.Web.Core.Models;

namespace Sample.Web.Mvc.Filters
{
    public class MemoryCacheFilter : ActionFilterAttribute
    {
        #region Fields

        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public MemoryCacheFilter(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Methods

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!_memoryCache.TryGetValue(MemoryCacheKeys.User, out User user))
            {
                var message = MemoryCacheKeys.NotLogin;

                if (_memoryCache.TryGetValue(MemoryCacheKeys.NotLogin, out string notlogin))
                    message = notlogin;

                context.Result = new JsonResult(new ServiceResult { Success = false, Message = message, Data = null });
            }

            base.OnActionExecuting(context);
        }

        #endregion Methods
    }
}