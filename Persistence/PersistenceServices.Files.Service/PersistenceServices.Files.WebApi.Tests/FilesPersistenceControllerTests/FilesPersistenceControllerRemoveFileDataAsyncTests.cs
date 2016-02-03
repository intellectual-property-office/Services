using System;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using PersistenceServices.Files.Domain.Interfaces;
using PersistenceServices.Files.WebApi.Controllers;

namespace PersistenceServices.Files.WebApi.Tests.FilesPersistenceControllerTests
{
    [TestFixture]
    [Category("Files Web Api")]
    public class FilesPersistenceControllerRemoveFileDataAsyncTests
    {
        [Test]
        public async Task RemoveFileDataAsyncMethodCallsCorrectServiceMethodTest()
        {
            var fileDataId = Guid.NewGuid();
            var mockFilesPersistenceService = new Mock<IFilesPersistenceService>();
            var persistenceController = new FilesPersistenceController(mockFilesPersistenceService.Object);

            await persistenceController.DeleteFileDataAsync(fileDataId);
            mockFilesPersistenceService.Verify(m => m.DeleteFileDataAsync(fileDataId), Times.Once());
        }

        [Test]
        public async Task RemoveFileDataAsyncMethodReturnsCorrectTypeTest()
        {
            var fileDataId = Guid.NewGuid();
            var mockFilesPersistenceService = new Mock<IFilesPersistenceService>();
            var persistenceController = new FilesPersistenceController(mockFilesPersistenceService.Object);

            var result = await persistenceController.DeleteFileDataAsync(fileDataId);
            Assert.AreEqual(typeof(OkResult), result.GetType());
        }
    }
}