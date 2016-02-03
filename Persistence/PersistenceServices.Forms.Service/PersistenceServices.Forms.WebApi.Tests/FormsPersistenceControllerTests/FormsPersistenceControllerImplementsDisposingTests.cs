using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersistenceServices.Forms.Domain.Interfaces;
using PersistenceServices.Forms.WebApi.Controllers;

namespace PersistenceServices.Forms.WebApi.Tests.FormsPersistenceControllerTests
{
    [TestClass]
    public class FormsPersistenceControllerImplementsDisposingTests
    {
        [TestMethod]
        public void FormsPersistenceControllerImplementsIdisposableTest()
        {
            var mockFormsPersistenceService = new Mock<IFormsPersistenceService>();

            using (new FormsPersistenceController(mockFormsPersistenceService.Object))
            {
            }
        }

        [TestMethod]
        public void FormsPersistenceControllerDisposesFormsPersistenceServiceTest()
        {
            var mockFormsPersistenceService = new Mock<IFormsPersistenceService>();

            using (new FormsPersistenceController(mockFormsPersistenceService.Object))
            {
            }

            mockFormsPersistenceService.Verify(m => m.Dispose(), Times.Once());
        }
    }
}