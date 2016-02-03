using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersistenceServices.Forms.Domain.Entities;
using PersistenceServices.Forms.Domain.Interfaces;
using PersistenceServices.Forms.Domain.Services;

namespace PersistenceServices.Forms.Domain.Tests.FormsPersistenceServiceTests
{
    [TestClass]
    public class FormsPersistenceServiceSaveFormDataAsyncTests
    {
        [TestMethod]
        public async Task SaveFormDataAsyncCallsCorrectUnitOfWorkMethodsIfNewTest()
        {
            var mockLoggerService = new Mock<ILoggerService>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()))
                .Returns(() => Task.FromResult<FormDataEntity>(null));

            var persistenceService = new FormsPersistenceService(mockUnitOfWork.Object, mockLoggerService.Object);
            await persistenceService.SaveFormDataAsync(It.IsAny<Guid>(), It.IsAny<string>());
            
            mockUnitOfWork.Verify(m => m.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()), Times.Once);
            mockUnitOfWork.Verify(m => m.Repository<FormDataEntity>().Add(It.IsAny<FormDataEntity>()), Times.Once);
            mockUnitOfWork.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public async Task SaveFormDataAsyncCallsCorrectUnitOfWorkMethodsIfUpdateTest()
        {
            var entity = new FormDataEntity();

            var mockLoggerService = new Mock<ILoggerService>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()))
            .Returns(() => Task.FromResult(entity));

            var persistenceService = new FormsPersistenceService(mockUnitOfWork.Object, mockLoggerService.Object);
            await persistenceService.SaveFormDataAsync(It.IsAny<Guid>(), It.IsAny<string>());

            mockUnitOfWork.Verify(m => m.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()), Times.Once);
            mockUnitOfWork.Verify(m => m.Repository<FormDataEntity>().Update(It.IsAny<FormDataEntity>()), Times.Once);
            mockUnitOfWork.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}