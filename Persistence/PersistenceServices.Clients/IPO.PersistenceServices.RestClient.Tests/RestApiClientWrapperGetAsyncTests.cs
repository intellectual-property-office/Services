using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using IPO.PersistenceServices.RestClient.Tests.Helpers;
using IPO.PersistenceServices.RestClient.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IPO.PersistenceServices.RestClient.Tests
{
    [TestClass]
    public class RestApiClientWrapperGetAsyncTests
    {
        [TestMethod]
        public async Task RestClientCallsCorrectRouteTest()
        {
            var responseMessage = HttpClientHelper.SetupHttpResponseMessage(HttpStatusCode.OK, new JsonContent(new List<TestResponseClass>()));
            var fakeMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var httpClient = HttpClientHelper.SetupHttpClient(fakeMessageHandler, new Uri("http://localhost/WebApi/"));
            var sut = new RestClient(httpClient);

            await sut.GetAsync<TestResponseClass>("GetAsync");

            Assert.AreEqual(fakeMessageHandler.Request.RequestUri, "http://localhost/WebApi/GetAsync");
        }

        [TestMethod]
        public async Task GetAsyncReturnsCorrectClassTypeTest()
        {
            var responseMessage = HttpClientHelper.SetupHttpResponseMessage(HttpStatusCode.OK, new JsonContent(new List<TestResponseClass>()));
            var fakeMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var httpClient = HttpClientHelper.SetupHttpClient(fakeMessageHandler);
            var sut = new RestClient(httpClient);

            var result = await sut.GetAsync<TestResponseClass>("GetAsync");
            Assert.AreEqual(typeof(List<TestResponseClass>), result.GetType());
        }

        [TestMethod]
        public async Task GetAsyncReturnsCorrectPopulatedClassTest()
        {
            var testResponseClassList = new List<TestResponseClass>
            {
                new TestResponseClass {ResponseContent = "content1"},
                new TestResponseClass {ResponseContent = "content2"},
                new TestResponseClass {ResponseContent = "content3"}
            };

            var responseMessage = HttpClientHelper.SetupHttpResponseMessage(HttpStatusCode.OK, new JsonContent(testResponseClassList));
            var fakeMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var httpClient = HttpClientHelper.SetupHttpClient(fakeMessageHandler);
            var sut = new RestClient(httpClient);

            var result = await sut.GetAsync<TestResponseClass>("GetAsync");
            var resultList = result.ToList();

            Assert.AreEqual(testResponseClassList[0].ResponseContent, resultList[0].ResponseContent);
            Assert.AreEqual(testResponseClassList[1].ResponseContent, resultList[1].ResponseContent);
            Assert.AreEqual(testResponseClassList[2].ResponseContent, resultList[2].ResponseContent);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public async Task GetAsyncReturnsHttpResponseExceptionTest()
        {
            var responseMessage = HttpClientHelper.SetupHttpResponseMessage(HttpStatusCode.NotFound, new JsonContent(new List<TestResponseClass>()));
            var fakeMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var httpClient = HttpClientHelper.SetupHttpClient(fakeMessageHandler);
            var sut = new RestClient(httpClient);

            await sut.GetAsync<object>("GetAsync");
        }
    }
}