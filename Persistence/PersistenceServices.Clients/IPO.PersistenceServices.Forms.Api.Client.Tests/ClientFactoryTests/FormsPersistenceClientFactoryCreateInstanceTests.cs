using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IPO.PersistenceServices.Forms.Api.Client.Tests.ClientFactoryTests
{
    [TestClass]
    public class FormsPersistenceClientFactoryCreateInstanceTests
    {
        [TestMethod]
        public void FormsPersistenceClientFactoryCreatesClientInstanceTest()
        {
            var formsPersistenceClientFactory = new FormsPersistenceClientFactory("http://localhost/IPO.Persistence.Forms.WebApi/");
            var formsPersistenceClient = formsPersistenceClientFactory.ClientInstance;

            Assert.IsInstanceOfType(formsPersistenceClient, typeof(FormsPersistenceClient));
        }
    }
}