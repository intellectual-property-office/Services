using System;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersistenceServices.Forms.Domain.Interfaces;
using PersistenceServices.Forms.WebApi.Controllers;

namespace PersistenceServices.Forms.WebApi.Tests.FormsPersistenceControllerTests
{
    [TestClass]
    public class FormsPersistenceControllerSaveFormDataAsyncTests
    {
        [TestMethod]
        public async Task SaveFormDataAsyncMethodCallsCorrectServiceMethodTest()
        {
            var formDataId = Guid.NewGuid();
            var serializedObject = string.Empty;
            var mockFormsPersistenceService = new Mock<IFormsPersistenceService>();
            var persistenceController = new FormsPersistenceController(mockFormsPersistenceService.Object);

            await persistenceController.SaveFormDataAsync(formDataId, serializedObject);
            mockFormsPersistenceService.Verify(m => m.SaveFormDataAsync(formDataId, serializedObject), Times.Once());
        }

        [TestMethod]
        public async Task SaveFormDataAsyncMethodReturnsCorrectTypeTest()
        {
            var formDataId = Guid.NewGuid();
            var serializedObject = string.Empty;
            var mockFormsPersistenceService = new Mock<IFormsPersistenceService>();
            var persistenceController = new FormsPersistenceController(mockFormsPersistenceService.Object);

            var result = await persistenceController.SaveFormDataAsync(formDataId, serializedObject);
            Assert.AreEqual(typeof(OkResult), result.GetType());
        }
    }
}