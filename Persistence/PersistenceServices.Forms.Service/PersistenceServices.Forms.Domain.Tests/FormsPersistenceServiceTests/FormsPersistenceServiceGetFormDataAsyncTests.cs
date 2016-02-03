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
    public class FormsPersistenceServiceGetFormDataAsyncTests
    {
        [TestMethod]
        public async Task GetFormDataAsyncCallsCorrectUnitOfWorkMethodTest()
        {
            var entity = new FormDataEntity();

            var mockLoggerService = new Mock<ILoggerService>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()))
                .Returns(() => Task.FromResult(entity));

            var persistenceService = new FormsPersistenceService(mockUnitOfWork.Object, mockLoggerService.Object);

            await persistenceService.GetFormDataAsync(It.IsAny<Guid>());
            mockUnitOfWork.Verify(m => m.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()), Times.Once);
        }

        [TestMethod]
        public async Task GetFormDataAsyncReturnsSerializedObjectTest()
        {
            var entity = new FormDataEntity { SerializedFormData = "SerializedObject" };

            var mockLoggerService = new Mock<ILoggerService>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()))
                .Returns(() => Task.FromResult(entity));

            var persistenceService = new FormsPersistenceService(mockUnitOfWork.Object, mockLoggerService.Object);

            var result = await persistenceService.GetFormDataAsync(It.IsAny<Guid>());
            Assert.AreEqual(typeof(string), result.GetType());
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task GetFormDataAsyncThrowsNotFoundExceptionIfEntityDoesNotExistTest()
        {
            var mockLoggerService = new Mock<ILoggerService>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()))
                .Returns(() => Task.FromResult<FormDataEntity>(null));
            var persistenceService = new FormsPersistenceService(mockUnitOfWork.Object, mockLoggerService.Object);

            await persistenceService.GetFormDataAsync(It.IsAny<Guid>());
        }
    }
}