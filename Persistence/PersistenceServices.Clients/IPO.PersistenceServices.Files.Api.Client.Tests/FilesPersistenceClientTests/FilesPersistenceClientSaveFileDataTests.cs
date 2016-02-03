using System.Threading.Tasks;
using IPO.PersistenceServices.Files.Api.Client;
using IPO.PersistenceServices.RestClient.Interfaces;
using Moq;
using NUnit.Framework;
using Persistence.Files.Client.Tests.Models;


namespace Persistence.Files.Client.Tests.FilesPersistenceClientTests
{
    [TestFixture]
    public class FilesPersistenceClientSaveFormDataTests
    {
        [Test]
        public async Task SaveFileDataAsyncCallsCorrectMethodsTest()
        {
            var mockWebApiClient = new Mock<IRestClient>();

            var filesPersistenceClient = new FilesPersistenceClient(mockWebApiClient.Object);

            var byteArray = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            var testRequest = new TestRequestClass { Bytes = byteArray, ContentType = "img", FileName = "MyTest.img" };

            await filesPersistenceClient.SaveFileDataAsync<TestRequestClass, TestResponseClass>(testRequest);
            mockWebApiClient.Verify(m => m.PostItemAsync<TestRequestClass, TestResponseClass>(It.IsAny<string>(), It.IsAny<TestRequestClass>()), Times.Once());
        }
    }
}