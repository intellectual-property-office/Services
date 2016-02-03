using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using IPO.PersistenceServices.RestClient.Tests.Helpers;
using IPO.PersistenceServices.RestClient.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IPO.PersistenceServices.RestClient.Tests
{
    [TestClass]
    public class RestApiClientWrapperGetItemAsyncTests
    {
        [TestMethod]
        public async Task RestClientCallsCorrectRouteTest()
        {
            var responseMessage = HttpClientHelper.SetupHttpResponseMessage(HttpStatusCode.OK, new JsonContent(new object()));
            var fakeMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var httpClient = HttpClientHelper.SetupHttpClient(fakeMessageHandler, new Uri("http://localhost/WebApi/"));
            var sut = new RestClient(httpClient);

            await sut.GetItemAsync<TestResponseClass>("GetItemAsync");

            Assert.AreEqual(fakeMessageHandler.Request.RequestUri, "http://localhost/WebApi/GetItemAsync");
        }

        [TestMethod]
        public async Task GetItemAsyncReturnsCorrectClassTypeTest()
        {
            var responseMessage = HttpClientHelper.SetupHttpResponseMessage(HttpStatusCode.OK, new JsonContent(new object()));
            var fakeMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var httpClient = HttpClientHelper.SetupHttpClient(fakeMessageHandler);
            var sut = new RestClient(httpClient);

            var result = await sut.GetItemAsync<TestResponseClass>("GetItemAsync");
            Assert.AreEqual(typeof(TestResponseClass), result.GetType());
        }

        [TestMethod]
        public async Task GetItemAsyncReturnsCorrectPopulatedClassTest()
        {
            var responseMessage = HttpClientHelper.SetupHttpResponseMessage(HttpStatusCode.OK, new JsonContent(new TestResponseClass { ResponseContent = "content" }));
            var fakeMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var httpClient = HttpClientHelper.SetupHttpClient(fakeMessageHandler);
            var sut = new RestClient(httpClient);

            var result = await sut.GetItemAsync<TestResponseClass>("GetItemAsync");
            Assert.AreEqual("content", result.ResponseContent);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public async Task GetItemAsyncReturnsHttpResponseExceptionTest()
        {
            var responseMessage = HttpClientHelper.SetupHttpResponseMessage(HttpStatusCode.NotFound, new StringContent(string.Empty));
            var fakeMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var httpClient = HttpClientHelper.SetupHttpClient(fakeMessageHandler);
            var sut = new RestClient(httpClient);

            await sut.GetItemAsync<object>("GetItemAsync");
        }       
    }
}