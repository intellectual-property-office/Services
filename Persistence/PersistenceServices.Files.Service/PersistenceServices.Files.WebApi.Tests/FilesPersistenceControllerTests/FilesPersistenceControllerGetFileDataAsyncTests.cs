using System;
using System.Threading.Tasks;
using System.Web.Http.Results;
using IPO.PersistenceServices.Files.Models;
using Moq;
using NUnit.Framework;
using PersistenceServices.Files.Domain.Interfaces;
using PersistenceServices.Files.WebApi.Controllers;

namespace PersistenceServices.Files.WebApi.Tests.FilesPersistenceControllerTests
{
    [TestFixture]
    [Category("Files Web Api")]
    public class FilesPersistenceControllerGetFileDataAsyncTests
    {
        [Test]
        public async Task GetFilesDataAsyncMethodCallsCorrectServiceMethodTest()
        {
            var fileDataId = Guid.NewGuid();
            var mockFilesPersistenceService = new Mock<IFilesPersistenceService>();
            var persistenceController = new FilesPersistenceController(mockFilesPersistenceService.Object);

            await persistenceController.GetFileDataAsync(fileDataId);
            mockFilesPersistenceService.Verify(m => m.GetFileDataAsync(fileDataId), Times.Once());
        }

        [Test]
        public async Task GetFilesDataAsyncMethodReturnsCorrectTypeTest()
        {
            var fileDataId = Guid.NewGuid();
            var mockFilesPersistenceService = new Mock<IFilesPersistenceService>();
            var persistenceController = new FilesPersistenceController(mockFilesPersistenceService.Object);

            var result = await persistenceController.GetFileDataAsync(fileDataId);
            Assert.AreEqual(typeof(OkNegotiatedContentResult<FilePersistenceServiceResponseDto>), result.GetType());
        }
    }
}