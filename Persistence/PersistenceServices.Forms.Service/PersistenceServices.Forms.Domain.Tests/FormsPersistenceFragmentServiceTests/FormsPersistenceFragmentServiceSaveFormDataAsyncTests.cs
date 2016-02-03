using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersistenceServices.Forms.Domain.Entities;
using PersistenceServices.Forms.Domain.Exceptions;
using PersistenceServices.Forms.Domain.Interfaces;
using PersistenceServices.Forms.Domain.Services;

namespace PersistenceServices.Forms.Domain.Tests.FormsPersistenceFragmentServiceTests
{
    [TestClass]
    public class FormsPersistenceFragmentServiceSaveFormDataAsyncTests
    {
        [TestMethod]
        public async Task SaveFormDataFragmentAsyncCallsCorrectUnitOfWorkMethodsTest()
        {
            var entity = new FormDataEntity { SerializedFormData = "SerializedObject" };
            var mockDataFragmentService = new Mock<IDataFragmentService>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()))
                .Returns(() => Task.FromResult(entity));

            var persistenceService = new FormsPersistenceFragmentService(mockUnitOfWork.Object, mockDataFragmentService.Object);
            await persistenceService.SaveFormDataFragmentAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<KeyValuePair<string, string>>(), It.IsAny<string>());

            mockUnitOfWork.Verify(m => m.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()), Times.Once);
            mockUnitOfWork.Verify(m => m.Repository<FormDataEntity>().Update(entity), Times.Once);
            mockUnitOfWork.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public async Task SaveFormDataFragmentAsyncCallsCorrectDataFragmentServiceMethodTest()
        {
            var entity = new FormDataEntity { SerializedFormData = "SerializedObject" };
            var mockDataFragmentService = new Mock<IDataFragmentService>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()))
                .Returns(() => Task.FromResult(entity));

            var persistenceService = new FormsPersistenceFragmentService(mockUnitOfWork.Object, mockDataFragmentService.Object);
            await persistenceService.SaveFormDataFragmentAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<KeyValuePair<string, string>>(), It.IsAny<string>());

            mockDataFragmentService.Verify(m => m.UpdateFragment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<KeyValuePair<string, string>>()));
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task SaveFormDataFragmentAsyncThrowsNotFoundExceptionIfEntityDoesNotExistTest()
        {
            var mockDataFragmentService = new Mock<IDataFragmentService>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()))
                .Returns(() => Task.FromResult<FormDataEntity>(null));

            var persistenceService = new FormsPersistenceFragmentService(mockUnitOfWork.Object, mockDataFragmentService.Object);

            await persistenceService.SaveFormDataFragmentAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<KeyValuePair<string, string>>(), It.IsAny<string>());
        }
    }
}
