using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IPO.PersistenceServices.RestClient.Tests.Helpers;
using IPO.PersistenceServices.RestClient.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IPO.PersistenceServices.RestClient.Tests
{
    [TestClass]
    public class RestApiClientWrapperTests
    {
        [TestMethod]
        public async Task RestClientImplementsJson()
        {
            var responseMessage = HttpClientHelper.SetupHttpResponseMessage(HttpStatusCode.OK, new JsonContent(new object()));
            var fakeMessageHandler = new FakeHttpMessageHandler(responseMessage);
            var httpClient = HttpClientHelper.SetupHttpClient(fakeMessageHandler);
            var sut = new RestClient(httpClient);

            await sut.GetItemAsync<TestResponseClass>("GetAsync");

            Assert.IsTrue(fakeMessageHandler.Request.Headers.Accept.Contains(new MediaTypeWithQualityHeaderValue("application/json")));
        }
    }
}