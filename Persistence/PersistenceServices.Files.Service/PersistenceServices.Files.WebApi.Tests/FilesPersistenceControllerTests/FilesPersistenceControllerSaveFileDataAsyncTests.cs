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
    public class FilesPersistenceControllerSaveFileDataAsyncTests
    {
        [Test]
        public async Task SaveFileDataAsyncMethodCallsCorrectServiceMethodTest()
        {
            var byteArray = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            var fileBlobServiceRequestDto = new FilePersistenceServiceRequestDto { Bytes = byteArray, ContentType = "img", FileName = "MyTest.img" };

            var mockFilesPersistenceService = new Mock<IFilesPersistenceService>();
            var persistenceController = new FilesPersistenceController(mockFilesPersistenceService.Object);

            var fileDataId = await persistenceController.SaveFileDataAsync(fileBlobServiceRequestDto);
            mockFilesPersistenceService.Verify(m => m.SaveFileDataAsync(fileBlobServiceRequestDto.Bytes, fileBlobServiceRequestDto.ContentType, fileBlobServiceRequestDto.FileName), Times.Once());
        }

        [Test]
        public async Task SaveFileDataAsyncMethodReturnsCorrectTypeTest()
        {
            var byteArray = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
            var fileBlobServiceRequestDto = new FilePersistenceServiceRequestDto { Bytes = byteArray, ContentType = "img", FileName = "MyTest.img" };

            var mockFilesPersistenceService = new Mock<IFilesPersistenceService>();
            var persistenceController = new FilesPersistenceController(mockFilesPersistenceService.Object);

            var result = await persistenceController.SaveFileDataAsync(fileBlobServiceRequestDto);
            Assert.AreEqual(typeof(OkNegotiatedContentResult<FilePersistenceServiceResponseDto>), result.GetType());
        }
    }
}