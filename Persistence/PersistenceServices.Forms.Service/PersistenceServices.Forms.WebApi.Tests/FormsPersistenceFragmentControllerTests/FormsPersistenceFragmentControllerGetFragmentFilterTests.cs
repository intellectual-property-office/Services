using System.Collections.Generic;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersistenceServices.Forms.Domain.Interfaces;
using PersistenceServices.Forms.WebApi.Controllers;

namespace PersistenceServices.Forms.WebApi.Tests.FormsPersistenceFragmentControllerTests
{
    [TestClass]
    public class FormsPersistenceFragmentControllerGetFragmentFilterTests
    {
        [TestMethod]
        public void GetFragmentFilterReturnsFilter()
        {
            var mockFormsPersistenceFragmentService = new Mock<IFormsPersistenceFragmentService>();
            var persistenceController = new FormsPersistenceFragmentController(mockFormsPersistenceFragmentService.Object);
            persistenceController.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/WebApi/?param1=1");
            var result = persistenceController.GetFragmentFilter();
             
            Assert.AreEqual("param1", result.Key);
            Assert.AreEqual("1", result.Value);
        }

        [TestMethod]
        public void GetFragmentFilterReturnsFirstFilterOnly()
        {
            var mockFormsPersistenceFragmentService = new Mock<IFormsPersistenceFragmentService>();
            var persistenceController = new FormsPersistenceFragmentController(mockFormsPersistenceFragmentService.Object);
            persistenceController.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/WebApi/?param1=1&param2=2");
            var result = persistenceController.GetFragmentFilter();

            Assert.AreEqual(typeof (KeyValuePair<string, string>), result.GetType());
            Assert.AreEqual("param1", result.Key);
            Assert.AreEqual("1", result.Value);
        }
    }
}