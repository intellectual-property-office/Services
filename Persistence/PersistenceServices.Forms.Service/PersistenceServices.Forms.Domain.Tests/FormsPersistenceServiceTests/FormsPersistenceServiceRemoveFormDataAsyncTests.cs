using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersistenceServices.Forms.Domain.Entities;
using PersistenceServices.Forms.Domain.Exceptions;
using PersistenceServices.Forms.Domain.Interfaces;
using PersistenceServices.Forms.Domain.Services;

namespace PersistenceServices.Forms.Domain.Tests.FormsPersistenceServiceTests
{
    [TestClass]
    public class FormsPersistenceServiceRemoveFormDataAsyncTests
    {
        [TestMethod]
        public async Task RemoveFormDataAsyncCallsCorrectUnitOfWorkMethodsTest()
        {
            var entity = new FormDataEntity();

            var mockLoggerService = new Mock<ILoggerService>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()))
                .Returns(() => Task.FromResult(entity));

            var persistenceService = new FormsPersistenceService(mockUnitOfWork.Object, mockLoggerService.Object);
            await persistenceService.RemoveFormDataAsync(It.IsAny<Guid>());
            
            mockUnitOfWork.Verify(m => m.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()), Times.Once);
            mockUnitOfWork.Verify(m => m.Repository<FormDataEntity>().Remove(entity), Times.Once);
            mockUnitOfWork.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task RemoveFormDataAsyncThrowsNotFoundExceptionIfEntityDoesNotExistTest()
        {
            var mockLoggerService = new Mock<ILoggerService>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()))
                .Returns(() => Task.FromResult<FormDataEntity>(null));

            var persistenceService = new FormsPersistenceService(mockUnitOfWork.Object, mockLoggerService.Object);

            await persistenceService.RemoveFormDataAsync(It.IsAny<Guid>());
        }
    }
}