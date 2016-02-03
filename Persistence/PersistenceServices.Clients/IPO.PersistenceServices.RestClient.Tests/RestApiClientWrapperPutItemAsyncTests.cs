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
    public class RestApiClientWrapperPutItemAsyncTests
    {
        [TestMethod]
        public async Task RestClientCallsCorrectRouteTest()
        {
            var responseMessage = HttpClientHelper.SetupHttpResponseMessage(HttpStatusCode.OK, new JsonContent(new object()));
            var fakeMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var httpClient = HttpClientHelper.SetupHttpClient(fakeMessageHandler, new Uri("http://localhost/WebApi/"));
            var sut = new RestClient(httpClient);

            await sut.PutItem("PutItemAsync", new TestResponseClass());

            Assert.AreEqual(fakeMessageHandler.Request.RequestUri, "http://localhost/WebApi/PutItemAsync");
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public async Task PutItemAsyncReturnsHttpResponseExceptionTest()
        {
            var responseMessage = HttpClientHelper.SetupHttpResponseMessage(HttpStatusCode.NotFound, new JsonContent(new Object()));
            var fakeMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var httpClient = HttpClientHelper.SetupHttpClient(fakeMessageHandler);
            var sut = new RestClient(httpClient);

            await sut.PutItem("PutItemAsync", new TestResponseClass());
        } 
    }
}