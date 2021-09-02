using AutoMapper;
using Sample.Core.Domain.Users;
using Sample.Services.Enums;
using Sample.Web.Core.Models.Users;

namespace Sample.Web.Core.Infrastructure.Mapper
{
    public class EnumMapper : IValueResolver<User, UserGridModel, string>
    {
        //private readonly IMemoryCache _memoryCache;
        private readonly IEnumManager _enumManager;

        public EnumMapper(IEnumManager enumManager)
        {
            //_memoryCache = memoryCache;
            _enumManager = enumManager;
        }

        public string Resolve(User source, UserGridModel destination, string destinationMember, ResolutionContext context)
        {
            return "aaa";
            //return _enumManager.GetDescription(source);
        }
    }
}