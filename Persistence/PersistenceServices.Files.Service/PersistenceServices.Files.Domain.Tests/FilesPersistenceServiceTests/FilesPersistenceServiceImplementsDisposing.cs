using Moq;
using NUnit.Framework;
using PersistenceServices.Files.Domain.Interfaces;
using PersistenceServices.Files.Domain.Services;

namespace PersistenceServices.Files.Domain.Tests.FilesPersistenceServiceTests
{
    [TestFixture]
    [Category("Files Domain")]
    public class FilesPersistenceServiceImplementsDisposing
    {
        [Test]
        public void FilesPersistenceServiceImplementsIdisposableTest()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockLoggerService = new Mock<ILoggerService>();

            using (new FilesPersistenceService(mockUnitOfWork.Object, mockLoggerService.Object))
            {
            }
        }

        [Test]
        public void FilesPersistenceServiceDisposesUnitOfWorkTest()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockLoggerService = new Mock<ILoggerService>();

            using (new FilesPersistenceService(mockUnitOfWork.Object, mockLoggerService.Object))
            {
            }

            mockUnitOfWork.Verify(m => m.Dispose(), Times.Once());
        }
    }
}