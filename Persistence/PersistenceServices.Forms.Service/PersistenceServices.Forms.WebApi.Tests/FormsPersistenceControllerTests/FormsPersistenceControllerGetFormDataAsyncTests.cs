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
    public class FormsPersistenceControllerGetFormDataAsyncTests
    {
        [TestMethod]
        public async Task GetFormDataAsyncMethodCallsCorrectServiceMethodTest()
        {
            var formDataId = Guid.NewGuid();
            var mockFormsPersistenceService = new Mock<IFormsPersistenceService>();

            var persistenceController = new FormsPersistenceController(mockFormsPersistenceService.Object);

            await persistenceController.GetFormDataAsync(formDataId);

            mockFormsPersistenceService.Verify(m => m.GetFormDataAsync(formDataId), Times.Once());
        }

        [TestMethod]
        public async Task GetFormDataAsyncMethodReturnsCorrectTypeTest()
        {
            var formDataId = Guid.NewGuid();
            var mockFormsPersistenceService = new Mock<IFormsPersistenceService>();

            var persistenceController = new FormsPersistenceController(mockFormsPersistenceService.Object);

            var result = await persistenceController.GetFormDataAsync(formDataId);
            Assert.AreEqual(typeof(OkNegotiatedContentResult<string>), result.GetType());
        }
    }
}
