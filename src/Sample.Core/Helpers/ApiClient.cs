using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Sample.Core.Helpers
{
    public class ApiClient
    {
        #region Fields

        private readonly HttpClient _httpClient;

        #endregion Fields

        #region Constructor

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #endregion Constructor

        #region Methods

        public string GetContent(string requestUri, string token)
        {
            AddToken(token);

            _httpClient.BaseAddress = new Uri(requestUri);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = _httpClient.GetAsync(requestUri);
            response.Wait();

            if (response != null && response.Result != null && response.Result.Content != null)
            {
                var result = response.Result.Content.ReadAsStringAsync();
                result.Wait();

                if (result != null)
                {
                    return result.Result;
                }
            }

            return null;
        }

        public Tuple<string, string> PostKeyValueContent(string requestUri, IEnumerable<KeyValuePair<string, string>> values)
        {
            if (values != null)
            {
                using (var content = new FormUrlEncodedContent(values))
                {
                    var response = _httpClient.PostAsync(requestUri, content);
                    response.Wait();

                    if (response != null && response.Result != null && response.Result.Content != null)
                    {
                        var result = response.Result.Content.ReadAsStringAsync();
                        result.Wait();

                        if (result != null)
                        {
                            var deserializedResult = JsonConvert.DeserializeObject<dynamic>(result.Result);

                            return new Tuple<string, string>(deserializedResult.access_token.ToString(), deserializedResult.expiration.ToString());
                        }
                    }
                }
            }

            return null;
        }

        public string PostJsonContent(string requestUri, HttpContent content, string token)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            AddToken(token);

            var response = _httpClient.PostAsync(requestUri, content);
            response.Wait();

            if (response != null && response.Result != null && response.Result.Content != null)
            {
                var result = response.Result.Content.ReadAsStringAsync();
                result.Wait();

                if (result != null)
                {
                    return result.Result;
                }
            }

            return null;
        }

        private void AddToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        #endregion Methods
    }
}