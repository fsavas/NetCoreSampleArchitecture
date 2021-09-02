using Microsoft.Extensions.Caching.Memory;
using Sample.Core.Defaults;
using Sample.Core.Helpers;
using System;

namespace Sample.Services.Rest
{
    public partial class TokenHandler : ITokenHandler
    {
        #region Fields

        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public TokenHandler(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Methods

        public string GetAccessToken()
        {
            if (_memoryCache.TryGetValue(MemoryCacheKeys.DashboardExpiration, out DateTime expiration) && DateTime.Now < expiration && _memoryCache.TryGetValue(MemoryCacheKeys.DashboardAccessToken, out string accessToken))
            {
                return accessToken;
            }

            return GetNewAccessToken();
        }

        private string GetNewAccessToken()
        {
            var tokenProperties = RestHelper.GetTokenProperties("url", "username", "password");//todo get from
            _memoryCache.Set(MemoryCacheKeys.DashboardAccessToken, tokenProperties.Item1);
            _memoryCache.Set(MemoryCacheKeys.DashboardExpiration, tokenProperties.Item2);

            return tokenProperties.Item1;
        }

        #endregion Methods
    }
}