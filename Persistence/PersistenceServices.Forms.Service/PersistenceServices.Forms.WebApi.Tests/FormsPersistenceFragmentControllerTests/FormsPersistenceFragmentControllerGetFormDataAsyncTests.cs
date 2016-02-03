using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersistenceServices.Forms.Domain.Interfaces;
using PersistenceServices.Forms.WebApi.Controllers;

namespace PersistenceServices.Forms.WebApi.Tests.FormsPersistenceFragmentControllerTests
{
    [TestClass]
    public class FormsPersistenceFragmentControllerGetFormDataAsyncTests
    {
        [TestMethod]
        public async Task GetFormDataFragmentAsyncMethodCallsCorrectServiceMethodTest()
        {
            var formDataId = Guid.NewGuid();
            var fragmentName = string.Empty;
            var mockFormsPersistenceFragmentService = new Mock<IFormsPersistenceFragmentService>();
            var persistenceController = new FormsPersistenceFragmentController(mockFormsPersistenceFragmentService.Object);

            await persistenceController.GetFormDataFragmentAsync(formDataId, fragmentName);
            mockFormsPersistenceFragmentService.Verify(m => m.GetFormDataFragmentAsync(formDataId, fragmentName, new KeyValuePair<string, string>()), Times.Once());
        }

        [TestMethod]
        public async Task GetFormDataFragmentAsyncMethodReturnsCorrectTypeTest()
        {
            var mockFormsPersistenceFragmentService = new Mock<IFormsPersistenceFragmentService>();
            var persistenceController = new FormsPersistenceFragmentController(mockFormsPersistenceFragmentService.Object);

            var result = await persistenceController.GetFormDataFragmentAsync(Guid.NewGuid(), string.Empty);
            Assert.AreEqual(typeof(OkNegotiatedContentResult<string>), result.GetType());
        }
    }
}