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
    public class FormsPersistenceClientGetFormDataTests
    {
        [TestMethod]
        public async Task GetFormDataAsyncCallsCorrectMethodsTest()
        {
            var mockHttpClient = new Mock<IRestClient>();
            var mockSerializationService = new Mock<ISerializationService>();

            var formsPersistenceClient = new FormsPersistenceClient(mockHttpClient.Object, mockSerializationService.Object);

            await formsPersistenceClient.GetFormDataAsync<TestResponseClass>(Guid.NewGuid());
            mockHttpClient.Verify(m => m.GetItemAsync<string>(It.IsAny<string>()), Times.Once());
            mockSerializationService.Verify(m => m.DeSerializeFormData<TestResponseClass>(It.IsAny<string>()));
        }

        [TestMethod]
        public async Task GetFormDataFragmentAsyncReturnsCorrectObjectTypeTest()
        {
            var responseClass = new TestResponseClass();
            var mockHttpClient = new Mock<IRestClient>();
            var mockSerializationService = new Mock<ISerializationService>();
            mockSerializationService.Setup(x => x.DeSerializeFormData<TestResponseClass>(It.IsAny<string>())).Returns(responseClass);

            var formsPersistenceClient = new FormsPersistenceClient(mockHttpClient.Object, mockSerializationService.Object);
            var result = await formsPersistenceClient.GetFormDataAsync<TestResponseClass>(Guid.NewGuid());

            Assert.AreEqual(typeof(TestResponseClass), result.GetType());
        }

        [TestMethod]
        public async Task GetFormDataAsyncReturnsCorrectObjectTest()
        {
            var responseClass = new TestResponseClass();
            var mockHttpClient = new Mock<IRestClient>();
            var mockSerializationService = new Mock<ISerializationService>();
            mockSerializationService.Setup(x => x.DeSerializeFormData<TestResponseClass>(It.IsAny<string>())).Returns(responseClass);

            var formsPersistenceClient = new FormsPersistenceClient(mockHttpClient.Object, mockSerializationService.Object);
            var result = await formsPersistenceClient.GetFormDataAsync<TestResponseClass>(Guid.NewGuid());

            Assert.AreEqual(result, responseClass);
        }
    }
}