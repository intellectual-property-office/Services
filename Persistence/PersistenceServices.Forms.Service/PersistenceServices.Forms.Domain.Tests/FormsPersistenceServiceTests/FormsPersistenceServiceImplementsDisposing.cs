using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersistenceServices.Forms.Domain.Interfaces;
using PersistenceServices.Forms.Domain.Services;

namespace PersistenceServices.Forms.Domain.Tests.FormsPersistenceServiceTests
{
    [TestClass]
    public class FormsPersistenceServiceImplementsDisposing
    {
        [TestMethod]
        public void FormsPersistenceServiceImplementsIdisposableTest()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockLoggerService = new Mock<ILoggerService>();

            using (new FormsPersistenceService(mockUnitOfWork.Object, mockLoggerService.Object))
            {
            }
        }

        [TestMethod]
        public void FormsPersistenceServiceDisposesUnitOfWorkTest()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockLoggerService = new Mock<ILoggerService>();

            using (new FormsPersistenceService(mockUnitOfWork.Object, mockLoggerService.Object))
            {
            }

            mockUnitOfWork.Verify(m => m.Dispose(), Times.Once());
        }
    }
}