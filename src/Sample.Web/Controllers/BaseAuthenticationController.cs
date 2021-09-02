using Microsoft.AspNetCore.Mvc;
using Sample.Core.Defaults;

namespace Sample.Web.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseAuthenticationController : ControllerBase
    {
        protected string successMessage = MemoryCacheKeys.ControllerActionSuccess;
    }
}