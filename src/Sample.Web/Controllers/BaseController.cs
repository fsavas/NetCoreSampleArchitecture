using Microsoft.AspNetCore.Mvc;
using Sample.Core.Defaults;
using Sample.Web.Mvc.Filters;

namespace Sample.Web.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(MemoryCacheFilter))]
    //returns automatic 400 error message while model validating
    //[ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected string successMessage = MemoryCacheKeys.ControllerActionSuccess;
    }
}