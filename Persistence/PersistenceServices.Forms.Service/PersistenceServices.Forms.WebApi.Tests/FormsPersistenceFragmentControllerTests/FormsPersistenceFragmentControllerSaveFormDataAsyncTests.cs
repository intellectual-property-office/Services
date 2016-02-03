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
    public class FormsPersistenceFragmentControllerSaveFormDataAsyncTests
    {
        [TestMethod]
        public async Task SaveFormDataAsyncMethodCallsCorrectServiceMethodTest()
        {
            var formDataId = Guid.NewGuid();
            var serializedObject = string.Empty;
            var fragmentName = string.Empty;
            var fragmentFilter = new KeyValuePair<string, string>();
            var mockFormsPersistenceFragmentService = new Mock<IFormsPersistenceFragmentService>();
            var persistenceController = new FormsPersistenceFragmentController(mockFormsPersistenceFragmentService.Object);

            await persistenceController.SaveFormDataFragmentAsync(formDataId, fragmentName, serializedObject);
            mockFormsPersistenceFragmentService.Verify(m => m.SaveFormDataFragmentAsync(formDataId, fragmentName, fragmentFilter, serializedObject), Times.Once());
        }

        [TestMethod]
        public async Task SaveFormDataAsyncMethodReturnsCorrectTypeTest()
        {
            var formDataId = Guid.NewGuid();
            var serializedObject = string.Empty;
            var fragmentName = string.Empty;
            var mockFormsPersistenceFragmentService = new Mock<IFormsPersistenceFragmentService>();
            var persistenceController = new FormsPersistenceFragmentController(mockFormsPersistenceFragmentService.Object);

            var result = await persistenceController.SaveFormDataFragmentAsync(formDataId, fragmentName, serializedObject);
            Assert.AreEqual(typeof(OkResult), result.GetType());
        }
    }
}