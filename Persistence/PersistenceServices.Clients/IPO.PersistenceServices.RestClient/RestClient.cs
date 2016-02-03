using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using IPO.PersistenceServices.RestClient.Interfaces;

namespace IPO.PersistenceServices.RestClient
{
    public class RestClient : IRestClient
    {
        private readonly HttpClient _httpClient;

        public RestClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            SetupClientHeaders(_httpClient);
        }

        public async Task<IEnumerable<T>> GetAsync<T>(string requestUri)
        {
            var response = await _httpClient.GetAsync(requestUri);
            return await HandleResponse<IEnumerable<T>>(response);
        }

        public async Task<T> GetItemAsync<T>(string requestUri)
        {
            var response = await _httpClient.GetAsync(requestUri);
            return await HandleResponse<T>(response);
        }

        public async Task PostItemAsync<T>(string requestUri, T item)
        {
            var response = await _httpClient.PostAsync(requestUri, item, new JsonMediaTypeFormatter());
            HandleResponse(response);
        }

        public async Task<T2> PostItemAsync<T1, T2>(string requestUri, T1 item)
        {
            var response = await _httpClient.PostAsync(requestUri, item, new JsonMediaTypeFormatter());
            return await HandleResponse<T2>(response);
        }

        public async Task PutItem<T>(string requestUri, T item)
        {
            var response = await _httpClient.PutAsync(requestUri, item, new JsonMediaTypeFormatter());
            HandleResponse(response);
        }

        public async Task DeleteItemAsync(string requestUri)
        {
            var response = await _httpClient.DeleteAsync(requestUri);
            HandleResponse(response);
        }

        private static async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            HandleResponse(response);
            return await response.Content.ReadAsAsync<T>();
        }

        private static void HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpResponseException(response);
            }
        }

        private static void SetupClientHeaders(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (ClaimsPrincipal.Current != null)
            {
                httpClient.DefaultRequestHeaders.Add("requestedBy", ClaimsPrincipal.Current.Identity.Name);
            }
        }
    }
}