using System.Collections.Generic;
using System.Linq;
using IPO.PersistenceServices.SerializationService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersistenceServices.Forms.Domain.Exceptions;
using PersistenceServices.Forms.Domain.Services;
using PersistenceServices.Forms.Domain.Tests.Models;

namespace PersistenceServices.Forms.Domain.Tests.DataFragmentServiceTests
{
    [TestClass]
    public class DataFragmentJsonServiceGetFragmentTests
    {
        [TestMethod]
        public void GetFormDataFragmentReturnsReturnsSerializedStringTest()
        {
            var testClass = GetTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);

            var result = dataFragmentService.GetFragment(serializedObject, "Owners", new KeyValuePair<string, string>());
            Assert.IsInstanceOfType(result, typeof(string));
        }

        [TestMethod]
        public void GetFormDataFragmentReturnsCorrectFragmentTest()
        {
            var testClass = GetTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);
            var targetFragmentserializedObject = serilializedService.SerializeFormData(testClass.Name);

            var result = dataFragmentService.GetFragment(serializedObject, "Name", new KeyValuePair<string, string>());
            Assert.AreEqual(targetFragmentserializedObject, result);
        }

        [TestMethod]
        public void GetFormDataArrayFragmentReturnsCorrectFragmentTest()
        {
            var testClass = GetTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);
            var targetFragmentserializedObject = serilializedService.SerializeFormData(testClass.Owners);

            var result = dataFragmentService.GetFragment(serializedObject, "Owners",
                new KeyValuePair<string, string>());
            Assert.AreEqual(targetFragmentserializedObject, result);
        }

        [TestMethod]
        public void GetFormDataFilteredArrayFragmentReturnsCorrectFilteredFragmentTest1()
        {
            var testClass = GetTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);
            var targetFragmentserializedObject = serilializedService.SerializeFormData(testClass.Owners.FirstOrDefault(a => a.Id == 2));
            var filterQuery = new KeyValuePair<string, string>("Id", "2");

            var result = dataFragmentService.GetFragment(serializedObject, "Owners", filterQuery);
            Assert.AreEqual(targetFragmentserializedObject, result);
        }

        [TestMethod]
        public void GetFormDataFilteredArrayFragmentReturnsCorrectFilteredFragmentTest2()
        {
            var testClass = GetTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);
            var targetFragmentserializedObject = serilializedService.SerializeFormData(testClass.Owners.FirstOrDefault(o => o.Address.Id == 2).Address);
            var filterQuery = new KeyValuePair<string, string>("Id", "2");

            var result = dataFragmentService.GetFragment(serializedObject, "Address", filterQuery);
            Assert.AreEqual(targetFragmentserializedObject, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetFormDataFragmentThrowsNotFoundExceptionTest()
        {
            var testClass = GetTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);

            dataFragmentService.GetFragment(serializedObject, "InvalidFragmentName", new KeyValuePair<string, string>());
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetFormDataFragmentFilteredThrowsNotFoundExceptionTest()
        {
            var testClass = GetTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);
            var filterQuery = new KeyValuePair<string, string>("Id", "3");

            dataFragmentService.GetFragment(serializedObject, "Addresses", filterQuery);
        }

        [TestMethod]
        [ExpectedException(typeof(UnsupportedOperationException))]
        public void GetFormDataFragmentInvalidJsonThrowsUnsupportedOperationExceptionTest()
        {
            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            const string serializedObject = "<xml>Not Json</xml>";

            dataFragmentService.GetFragment(serializedObject, "Addresses", new KeyValuePair<string, string>());
        }

        private static TestSerializableClass GetTestClass()
        {
            return new TestSerializableClass
            {
                Id = 1,

                Name = "TestClass",

                Owners = new List<TestSerializableOwnerClass>
                {
                    new TestSerializableOwnerClass
                    {
                        Id = 1,
                        Address = new TestSerializableAddressClass
                        {
                            Id = 1,
                            Addressline1 = "Test address 2",
                            Town = "Test town 2",
                            Postcode = "Test postcode 2"
                        }
                    },

                    new TestSerializableOwnerClass
                    {
                        Id = 2,
                        Address = new TestSerializableAddressClass
                        {
                            Id = 2,
                            Addressline1 = "Test address 2",
                            Town = "Test town 2",
                            Postcode = "Test postcode 2"
                        }
                    }
                }
            };
        }
    }
}
