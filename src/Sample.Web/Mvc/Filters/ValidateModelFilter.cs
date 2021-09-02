using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Sample.Core.Defaults;
using Sample.Core.Domain.Localizations;
using Sample.Web.Core.Models;
using System.Linq;

namespace Sample.Web.Mvc.Filters
{
    public class ValidateModelFilter : ActionFilterAttribute
    {
        #region Fields

        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public ValidateModelFilter(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Methods

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                if (context.ModelState.ErrorCount > 0)
                {
                    //string messages = string.Join("; ", context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                    string messages = "";
                    var messageList = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                    if (_memoryCache.TryGetValue(MemoryCacheKeys.Language, out Language language)) ;

                    foreach (var message in messageList)
                    {
                        if (language != null)
                        {
                            if (_memoryCache.TryGetValue(string.Join(MemoryCacheKeys.KeySeperator, message, language.Id), out string error))
                            {
                                messages = string.Join("! ", messages, error);
                                continue;
                            }
                        }

                        messages = string.Join("! ", messages, message);
                    }

                    context.Result = new JsonResult(new ServiceResult { Success = false, Message = messages, Data = null });
                }
                else
                    context.Result = new BadRequestObjectResult(context.ModelState);
            }

            base.OnActionExecuting(context);
        }

        #endregion Methods
    }
}