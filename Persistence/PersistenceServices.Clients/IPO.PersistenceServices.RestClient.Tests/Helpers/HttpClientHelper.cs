using System;
using System.Net;
using System.Net.Http;

namespace IPO.PersistenceServices.RestClient.Tests.Helpers
{
    public static class HttpClientHelper
    {
        public static HttpClient SetupHttpClient(FakeHttpMessageHandler fakeHttpMessageHandler, Uri baseUri = null)
        {
            return new HttpClient(fakeHttpMessageHandler)
            {
                BaseAddress = baseUri ?? new Uri("http://localhost/WebApi/")
            };
        }

        public static HttpResponseMessage SetupHttpResponseMessage(HttpStatusCode statusCode, HttpContent content)
        {
            return new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = content,
            };
        }
    }
}