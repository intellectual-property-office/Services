using Moq;
using NUnit.Framework;
using PersistenceServices.Files.Domain.Interfaces;
using PersistenceServices.Files.WebApi.Controllers;

namespace PersistenceServices.Files.WebApi.Tests.FIlesPersistenceControllerTests
{
    [TestFixture]
    [Category("Files Web Api")]
    public class FilesPersistenceControllerImplementsDisposingTests
    {
        [Test]
        public void FilesPersistenceControllerImplementsIdisposableTest()
        {
            var mockFilesPersistenceService = new Mock<IFilesPersistenceService>();

            using (new FilesPersistenceController(mockFilesPersistenceService.Object))
            {
            }
        }
    }
}