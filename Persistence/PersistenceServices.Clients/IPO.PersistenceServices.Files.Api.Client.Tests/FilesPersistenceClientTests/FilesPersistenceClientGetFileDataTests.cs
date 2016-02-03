using System;
using System.Threading.Tasks;
using IPO.PersistenceServices.Files.Api.Client;
using IPO.PersistenceServices.RestClient.Interfaces;
using Moq;
using NUnit.Framework;
using Persistence.Files.Client.Tests.Models;

namespace Persistence.Files.Client.Tests.FilesPersistenceClientTests
{
    [TestFixture]
    public class FilesPersistenceClientGetFormDataTests
    {
        [Test]
        public async Task GetFileDataAsyncCallsCorrectMethodsTest()
        {
            var mockHttpClient = new Mock<IRestClient>();

            var filesPersistenceClient = new FilesPersistenceClient(mockHttpClient.Object);

            await filesPersistenceClient.GetFileDataAsync<TestResponseClass>(Guid.NewGuid());
            mockHttpClient.Verify(m => m.GetItemAsync<TestResponseClass>(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public async Task GetFileDataAsyncReturnsCorrectObjectTest()
        {
            var responseClass = new TestResponseClass();
            var mockHttpClient = new Mock<IRestClient>();
            mockHttpClient.Setup(x => x.GetItemAsync<TestResponseClass>(It.IsAny<string>())).Returns(Task.FromResult(responseClass));
            
            var filesPersistenceClient = new FilesPersistenceClient(mockHttpClient.Object);
            var result = await filesPersistenceClient.GetFileDataAsync<TestResponseClass>(Guid.NewGuid());

            Assert.AreEqual(responseClass, result);
        }
    }
}