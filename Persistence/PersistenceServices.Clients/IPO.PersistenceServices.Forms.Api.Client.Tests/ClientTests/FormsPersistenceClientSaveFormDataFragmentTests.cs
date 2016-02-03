using System;
using System.Threading.Tasks;
using IPO.PersistenceServices.Forms.Api.Client.Tests.Models;
using IPO.PersistenceServices.RestClient.Interfaces;
using IPO.PersistenceServices.SerializationService.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IPO.PersistenceServices.Forms.Api.Client.Tests.ClientTests
{
    [TestClass]
    public class FormsPersistenceClientSaveFormDataFragmentTests
    {
        [TestMethod]
        public async Task SaveFormDataFragmentAsyncCallsCorrectMethodsTest()
        {
            var mockHttpClient = new Mock<IRestClient>();
            var mockSerializationService = new Mock<ISerializationService>();

            var formsPersistenceClient = new FormsPersistenceClient(mockHttpClient.Object, mockSerializationService.Object);

            await formsPersistenceClient.SaveFormDataFragmentAsync(Guid.NewGuid(), It.IsAny<TestResponseClass>(), It.IsAny<string>());
            mockSerializationService.Verify(m => m.SerializeFormData(It.IsAny<TestResponseClass>()));
            mockHttpClient.Verify(m => m.PostItemAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }
    }
}