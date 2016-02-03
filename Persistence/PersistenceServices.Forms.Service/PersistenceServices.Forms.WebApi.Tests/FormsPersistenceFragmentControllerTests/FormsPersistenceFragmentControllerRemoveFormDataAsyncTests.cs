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
    public class FormsPersistenceFragmentControllerRemoveFormDataAsyncTests
    {
        [TestMethod]
        public async Task RemoveFormDataAsyncMethodCallsCorrectServiceMethodTest()
        {
            var formDataId = Guid.NewGuid();
            var fragmentName = string.Empty;
            var fragmentFilter = new KeyValuePair<string, string>();
            var mockFormsPersistenceFragmentService = new Mock<IFormsPersistenceFragmentService>();
            var persistenceController = new FormsPersistenceFragmentController(mockFormsPersistenceFragmentService.Object);

            await persistenceController.RemoveFormFragmentDataAsync(formDataId, fragmentName);
            mockFormsPersistenceFragmentService.Verify(m => m.RemoveFormDataFragmentAsync(formDataId, fragmentName, fragmentFilter), Times.Once());
        }

        [TestMethod]
        public async Task RemoveFormDataAsyncMethodReturnsCorrectTypeTest()
        {
            var formDataId = Guid.NewGuid();
            var fragmentName = string.Empty;
            var mockFormsPersistenceFragmentService = new Mock<IFormsPersistenceFragmentService>();
            var persistenceController = new FormsPersistenceFragmentController(mockFormsPersistenceFragmentService.Object);

            var result = await persistenceController.RemoveFormFragmentDataAsync(formDataId, fragmentName);
            Assert.AreEqual(typeof(OkResult), result.GetType());
        }
    }
}