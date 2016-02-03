using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PersistenceServices.Files.Domain.Entities;
using PersistenceServices.Files.Domain.Exceptions;
using PersistenceServices.Files.Domain.Interfaces;
using PersistenceServices.Files.Domain.Services;

namespace PersistenceServices.Files.Domain.Tests.FilesPersistenceServiceTests
{
    [TestFixture]
    [Category("Files Domain")]
    public class FilesPersistenceServiceGetFileDataAsyncTests
    {
        [Test]
        public async Task GetFileDataAsyncCallsCorrectUnitOfWorkMethodTest()
        {
            var entity = new FileBlob();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockLoggerService = new Mock<ILoggerService>();

            mockUnitOfWork.Setup(x => x.Repository<FileBlob>().GetAsync(It.IsAny<Expression<Func<FileBlob, bool>>>()))
                .Returns(() => Task.FromResult(entity));

            var persistenceService = new FilesPersistenceService(mockUnitOfWork.Object, mockLoggerService.Object);

            await persistenceService.GetFileDataAsync(It.IsAny<Guid>());
            mockUnitOfWork.Verify(m => m.Repository<FileBlob>().GetAsync(It.IsAny<Expression<Func<FileBlob, bool>>>()), Times.Once);
        }

        [Test]
        public void GetFileDataAsyncThrowsNotFoundExceptionIfEntityDoesNotExistTest()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockLoggerService = new Mock<ILoggerService>();

            mockUnitOfWork.Setup(x => x.Repository<FileBlob>().GetAsync(It.IsAny<Expression<Func<FileBlob, bool>>>()))
                .Returns(() => Task.FromResult<FileBlob>(null));
            var persistenceService = new FilesPersistenceService(mockUnitOfWork.Object, mockLoggerService.Object);

            Assert.Throws<NotFoundException>(async () => await persistenceService.GetFileDataAsync(It.IsAny<Guid>()));
        }
    }
}