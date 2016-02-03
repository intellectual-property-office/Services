using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using IPO.PersistenceServices.RestClient.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IPO.PersistenceServices.RestClient.Tests
{
    [TestClass]
    public class RestClientWrapperDeleteItemAsyncTests
    {
        [TestMethod]
        public async Task RestClientCallsCorrectRouteTest()
        {
            var responseMessage = HttpClientHelper.SetupHttpResponseMessage(HttpStatusCode.OK, new JsonContent(new object()));
            var fakeMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var httpClient = HttpClientHelper.SetupHttpClient(fakeMessageHandler, new Uri("http://localhost/WebApi/"));
            var sut = new RestClient(httpClient);

            await sut.DeleteItemAsync("DeleteItemAsync");

            Assert.AreEqual(fakeMessageHandler.Request.RequestUri, "http://localhost/WebApi/DeleteItemAsync");
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public async Task DeleteItemAsyncReturnsHttpResponseExceptionTest()
        {
            var responseMessage = HttpClientHelper.SetupHttpResponseMessage(HttpStatusCode.NotFound, new JsonContent(new Object()));
            var fakeMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var httpClient = HttpClientHelper.SetupHttpClient(fakeMessageHandler);
            var sut = new RestClient(httpClient);

            await sut.DeleteItemAsync("DeleteItemAsync");
        }
    }
}