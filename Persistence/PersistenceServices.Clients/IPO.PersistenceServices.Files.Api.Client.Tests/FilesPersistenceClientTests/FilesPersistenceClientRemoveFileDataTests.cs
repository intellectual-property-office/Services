using System;
using System.Threading.Tasks;
using IPO.PersistenceServices.Files.Api.Client;
using IPO.PersistenceServices.RestClient.Interfaces;
using Moq;
using NUnit.Framework;

namespace Persistence.Files.Client.Tests.FilesPersistenceClientTests
{
    [TestFixture]
    public class FilesPersistenceClientRemoveFileDataTests
    {
        [Test]
        public async Task RemoveFileDataAsyncCallsCorrectMethodsTest()
        {
            var mockHttpClient = new Mock<IRestClient>();

            var formsPersistenceClient = new FilesPersistenceClient(mockHttpClient.Object);

            await formsPersistenceClient.RemoveFileDataAsync(Guid.NewGuid());
            mockHttpClient.Verify(m => m.DeleteItemAsync(It.IsAny<string>()), Times.Once());
        }
    }
}