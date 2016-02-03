using IPO.PersistenceServices.Files.Api.Client;
using NUnit.Framework;

namespace Persistence.Files.Client.Tests.FilesPersistenceClientFactoryTests
{
    [TestFixture]
    public class FilesPersistenceClientFactoryCreateInstanceTests
    {
        [Test]
        public void FilesPersistenceClientFactoryCreatesClientInstanceTest()
        {
            var filesPersistenceClientFactory = new FilesPersistenceClientFactory("http://localhost/Persistence.Files.WebApi/");
            var filesPersistenceClient = filesPersistenceClientFactory.GetClientInstance();

            Assert.IsInstanceOf<FilesPersistenceClient>(filesPersistenceClient);
        }
    }
}