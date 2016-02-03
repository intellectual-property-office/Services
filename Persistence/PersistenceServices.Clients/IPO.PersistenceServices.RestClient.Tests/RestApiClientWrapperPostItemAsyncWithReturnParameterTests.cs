using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using IPO.PersistenceServices.RestClient.Tests.Helpers;
using IPO.PersistenceServices.RestClient.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IPO.PersistenceServices.RestClient.Tests
{
    [TestClass]
    public class RestApiClientWrapperPostItemAsyncWithReturnParameterTests
    {
        [TestMethod]
        public async Task RestClientCallsCorrectRouteTest()
        {
            var responseMessage = HttpClientHelper.SetupHttpResponseMessage(HttpStatusCode.OK, new JsonContent("Test response"));
            var fakeMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var httpClient = HttpClientHelper.SetupHttpClient(fakeMessageHandler, new Uri("http://localhost/WebApi/"));
            var sut = new RestClient(httpClient);

            await sut.PostItemAsync<TestResponseClass, string>("PostItemAsync", new TestResponseClass());

            Assert.AreEqual(fakeMessageHandler.Request.RequestUri, "http://localhost/WebApi/PostItemAsync");
        }

        [TestMethod]
        public async Task PostItemAsyncReturnsCorrectTypeTest()
        {
            var responseMessage = HttpClientHelper.SetupHttpResponseMessage(HttpStatusCode.OK, new JsonContent("Test response"));
            var fakeMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var httpClient = HttpClientHelper.SetupHttpClient(fakeMessageHandler, new Uri("http://localhost/WebApi/"));
            var sut = new RestClient(httpClient);

            var result = await sut.PostItemAsync<TestResponseClass, string>("PostItemAsync", new TestResponseClass());
            Assert.AreEqual(typeof(string), result.GetType());
        }

        [TestMethod]
        public async Task PostItemAsyncReturnsCorrectResponseTest()
        {
            var responseMessage = HttpClientHelper.SetupHttpResponseMessage(HttpStatusCode.OK, new JsonContent("Test response"));
            var fakeMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var httpClient = HttpClientHelper.SetupHttpClient(fakeMessageHandler, new Uri("http://localhost/WebApi/"));
            var sut = new RestClient(httpClient);

            var result = await sut.PostItemAsync<TestResponseClass, string>("PostItemAsync", new TestResponseClass());
            Assert.AreEqual("Test response", result);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public async Task PostItemAsyncReturnsHttpResponseExceptionTest()
        {
            var responseMessage = HttpClientHelper.SetupHttpResponseMessage(HttpStatusCode.NotFound, new JsonContent("Test response"));
            var fakeMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var httpClient = HttpClientHelper.SetupHttpClient(fakeMessageHandler);
            var sut = new RestClient(httpClient);

            await sut.PostItemAsync<TestResponseClass, string>("PostItemAsync", new TestResponseClass());
        }  
    }
}