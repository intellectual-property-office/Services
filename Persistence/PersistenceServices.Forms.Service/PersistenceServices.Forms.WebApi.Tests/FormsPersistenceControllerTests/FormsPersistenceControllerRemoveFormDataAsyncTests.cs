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
    public class FormsPersistenceControllerRemoveFormDataAsyncTests
    {
        [TestMethod]
        public async Task RemoveFormDataAsyncMethodCallsCorrectServiceMethodTest()
        {
            var formDataId = Guid.NewGuid();
            var mockFormsPersistenceService = new Mock<IFormsPersistenceService>(); 
            var persistenceController = new FormsPersistenceController(mockFormsPersistenceService.Object);

            await persistenceController.RemoveFormDataAsync(formDataId);
            mockFormsPersistenceService.Verify(m => m.RemoveFormDataAsync(formDataId), Times.Once());
        }

        [TestMethod]
        public async Task RemoveFormDataAsyncMethodReturnsCorrectTypeTest()
        {
            var formDataId = Guid.NewGuid();
            var mockFormsPersistenceService = new Mock<IFormsPersistenceService>();
            var persistenceController = new FormsPersistenceController(mockFormsPersistenceService.Object);

            var result = await persistenceController.RemoveFormDataAsync(formDataId);
            Assert.AreEqual(typeof(OkResult), result.GetType());
        }
    }
}