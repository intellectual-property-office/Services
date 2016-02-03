using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersistenceServices.Forms.Domain.Interfaces;
using PersistenceServices.Forms.WebApi.Controllers;

namespace PersistenceServices.Forms.WebApi.Tests.FormsPersistenceFragmentControllerTests
{
    [TestClass]
    public class FormsPersistenceFragmentControllerImplementsDisposingTests
    {
        [TestMethod]
        public void FormsPersistenceFragmentControllerImplementsIdisposableTest()
        {
            var mockFormsPersistenceFragmentService = new Mock<IFormsPersistenceFragmentService>();

            using (new FormsPersistenceFragmentController(mockFormsPersistenceFragmentService.Object))
            {
            }
        }

        [TestMethod]
        public void FormsPersistenceFragmentControllerDisposesFormsPersistenceServiceTests()
        {
            var mockFormsPersistenceFragmentService = new Mock<IFormsPersistenceFragmentService>();

            using (new FormsPersistenceFragmentController(mockFormsPersistenceFragmentService.Object))
            {
            }

            mockFormsPersistenceFragmentService.Verify(m => m.Dispose(), Times.Once());
        }
    }
}