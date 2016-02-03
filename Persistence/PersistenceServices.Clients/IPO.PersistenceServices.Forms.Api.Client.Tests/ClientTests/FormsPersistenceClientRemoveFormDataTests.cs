using System;
using System.Threading.Tasks;
using IPO.PersistenceServices.RestClient.Interfaces;
using IPO.PersistenceServices.SerializationService.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IPO.PersistenceServices.Forms.Api.Client.Tests.ClientTests
{
    [TestClass]
    public class FormsPersistenceClientRemoveFormDataTests
    {
        [TestMethod]
        public async Task RemoveFormDataAsyncCallsCorrectMethodsTest()
        {
            var mockHttpClient = new Mock<IRestClient>();
            var mockSerializationService = new Mock<ISerializationService>();

            var formsPersistenceClient = new FormsPersistenceClient(mockHttpClient.Object, mockSerializationService.Object);

            await formsPersistenceClient.RemoveFormDataAsync(Guid.NewGuid());
            mockHttpClient.Verify(m => m.DeleteItemAsync(It.IsAny<string>()), Times.Once());
        }
    }
}