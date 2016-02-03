using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PersistenceServices.Files.Domain.Entities;
using PersistenceServices.Files.Domain.Interfaces;
using PersistenceServices.Files.Domain.Services;

namespace PersistenceServices.Files.Domain.Tests.FilesPersistenceServiceTests
{
    [TestFixture]
    [Category("Files Domain")]
    public class FilesPersistenceServiceSaveFileDataAsyncTests
    {
        [Test]
        public async Task SaveFileDataAsyncCallsCorrectUnitOfWorkMethodsIfNewTest()
        {      
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockLoggerService = new Mock<ILoggerService>();

            mockUnitOfWork.Setup(x => x.Repository<FileBlob>().GetAllAsync(It.IsAny<Expression<Func<FileBlob, bool>>>()))
                .Returns(() => Task.FromResult<IEnumerable<FileBlob>>(null));

            var persistenceService = new FilesPersistenceService(mockUnitOfWork.Object, mockLoggerService.Object);
            var byteArray = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            await persistenceService.SaveFileDataAsync(byteArray, "img", "MyTest.img");

            mockUnitOfWork.Verify(m => m.Repository<FileBlob>().Add(It.IsAny<FileBlob>()), Times.Once);
            mockUnitOfWork.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}