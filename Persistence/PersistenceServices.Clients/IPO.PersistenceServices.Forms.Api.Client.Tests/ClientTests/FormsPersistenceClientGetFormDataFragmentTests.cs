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
    public class FormsPersistenceClientGetFormDataFragmentTests
    {
        [TestMethod]
        public async Task GetFormDataFragmentAsyncCallsCorrectMethodsTest()
        {
            var mockHttpClient = new Mock<IRestClient>();
            var mockSerializationService = new Mock<ISerializationService>();

            var formsPersistenceClient = new FormsPersistenceClient(mockHttpClient.Object, mockSerializationService.Object);

            await formsPersistenceClient.GetFormDataFragmentAsync<TestResponseClass>(Guid.NewGuid(), string.Empty);
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
            var result = await formsPersistenceClient.GetFormDataFragmentAsync<TestResponseClass>(Guid.NewGuid(), string.Empty);

            Assert.AreEqual(typeof(TestResponseClass), result.GetType());
        }

        [TestMethod]
        public async Task GetFormDataFragmentAsyncReturnsCorrectObjectTest()
        {
            var responseClass = new TestResponseClass();
            var mockHttpClient = new Mock<IRestClient>();
            var mockSerializationService = new Mock<ISerializationService>();
            mockSerializationService.Setup(x => x.DeSerializeFormData<TestResponseClass>(It.IsAny<string>())).Returns(responseClass);

            var formsPersistenceClient = new FormsPersistenceClient(mockHttpClient.Object, mockSerializationService.Object);
            var result = await formsPersistenceClient.GetFormDataFragmentAsync<TestResponseClass>(Guid.NewGuid(), string.Empty);

            Assert.AreEqual(result, responseClass);
        }
    }
}