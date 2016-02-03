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
    public class FormsPersistenceFragmentServiceGetFormDataAsyncTests
    {
        [TestMethod]
        public async Task GetFormDataFragmentAsyncCallsCorrectUnitOfWorkMethodTest()
        {
            var entity = new FormDataEntity {SerializedFormData = "SerializedObject"};
            var mockDataFragmentService = new Mock<IDataFragmentService>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()))
                .Returns(() => Task.FromResult(entity));

            var persistenceService = new FormsPersistenceFragmentService(mockUnitOfWork.Object, mockDataFragmentService.Object);

            await persistenceService.GetFormDataFragmentAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<KeyValuePair<string, string>>());
            mockUnitOfWork.Verify(m => m.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()), Times.Once);
        }

        [TestMethod]
        public async Task GetFormDataFragmentAsyncCallsCorrectServiceMethodsTest()
        {
            var entity = new FormDataEntity { SerializedFormData = "SerializedObject" };
            var mockDataFragmentService = new Mock<IDataFragmentService>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()))
                .Returns(() => Task.FromResult(entity));

            var persistenceService = new FormsPersistenceFragmentService(mockUnitOfWork.Object, mockDataFragmentService.Object);

            await persistenceService.GetFormDataFragmentAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<KeyValuePair<string, string>>());
            mockDataFragmentService.Verify(m => m.GetFragment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<KeyValuePair<string, string>>()));
        }

        [TestMethod]
        public async Task GetFormDataFragmentAsyncReturnsSerializedObjectTest()
        {
            var entity = new FormDataEntity { SerializedFormData = "SerializedObject" };
            
            var mockDataFragmentService = new Mock<IDataFragmentService>();
            mockDataFragmentService.Setup(x => x.GetFragment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<KeyValuePair<string, string>>()))
                 .Returns(entity.SerializedFormData);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()))
                .Returns(() => Task.FromResult(entity));

            var persistenceService = new FormsPersistenceFragmentService(mockUnitOfWork.Object, mockDataFragmentService.Object);

            var result = await persistenceService.GetFormDataFragmentAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<KeyValuePair<string, string>>());
            Assert.AreEqual(typeof(string), result.GetType());
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task GetFormDataFragmentAsyncThrowsNotFoundExceptionIfEntityDoesNotExistTest()
        {
            var mockDataFragmentService = new Mock<IDataFragmentService>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Repository<FormDataEntity>().GetAsync(It.IsAny<Expression<Func<FormDataEntity, bool>>>()))
                .Returns(() => Task.FromResult<FormDataEntity>(null));

            var persistenceService = new FormsPersistenceFragmentService(mockUnitOfWork.Object, mockDataFragmentService.Object);

            await persistenceService.GetFormDataFragmentAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<KeyValuePair<string, string>>());
        }
    }
}