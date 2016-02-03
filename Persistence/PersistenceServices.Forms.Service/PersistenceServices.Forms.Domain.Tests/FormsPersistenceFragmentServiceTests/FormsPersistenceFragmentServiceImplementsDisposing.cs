using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersistenceServices.Forms.Domain.Interfaces;
using PersistenceServices.Forms.Domain.Services;

namespace PersistenceServices.Forms.Domain.Tests.FormsPersistenceFragmentServiceTests
{
    [TestClass]
    public class FormsPersistenceFragmentServiceImplementsDisposing
    {
        [TestMethod]
        public void FormsPersistenceServiceImplementsIdisposableTest()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockDataFragmentService = new Mock<IDataFragmentService>();

            using (new FormsPersistenceFragmentService(mockUnitOfWork.Object, mockDataFragmentService.Object))
            {
            }
        }

        [TestMethod]
        public void FormsPersistenceServiceDisposesUnitOfWorkTests()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockDataFragmentService = new Mock<IDataFragmentService>();

            using (new FormsPersistenceFragmentService(mockUnitOfWork.Object, mockDataFragmentService.Object))
            {
            }

            mockUnitOfWork.Verify(m => m.Dispose(), Times.Once());
        }
    }
}