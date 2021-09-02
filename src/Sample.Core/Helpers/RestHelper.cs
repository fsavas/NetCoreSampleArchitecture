using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Sample.Core.Helpers
{
    public static class RestHelper
    {
        #region Methods

        public static Tuple<string, string> GetTokenProperties(string url, string userName, string password)
        {
            using (var httpClient = new HttpClient())
            {
                var apiClient = new ApiClient(httpClient);

                return apiClient.PostKeyValueContent(url, values: new Dictionary<string, string> {
                    { "grant_type", "password" },
                    { "username", userName },
                    { "password", password }
                });
            }
        }

        public static string Post(string url, string token, object serializeObject)
        {
            if (!string.IsNullOrEmpty(token))
            {
                using (var httpClient = new HttpClient())
                {
                    var apiClient = new ApiClient(httpClient);
                    var json = JsonConvert.SerializeObject(serializeObject);
                    HttpContent content = new StringContent(json);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    return apiClient.PostJsonContent(url, content, token);
                }
            }

            return null;
        }

        public static string Get(string url, string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                using (var httpClient = new HttpClient())
                {
                    var apiClient = new ApiClient(httpClient);
                    return apiClient.GetContent(url, token);
                }
            }

            return null;
        }

        #endregion Methods
    }
}