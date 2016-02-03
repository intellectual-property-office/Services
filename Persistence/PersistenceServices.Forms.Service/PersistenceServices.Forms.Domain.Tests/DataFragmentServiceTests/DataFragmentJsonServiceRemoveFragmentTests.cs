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
    public class DataFragmentJsonServiceRemoveFragmentTests
    {
        [TestMethod]
        public void RemoveFormDataFragmentReturnsSerializedStringTest()
        {
            var testClass = GetTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);

            var result = dataFragmentService.RemoveFragment(serializedObject, "Owners", new KeyValuePair<string, string>());
            Assert.IsInstanceOfType(result, typeof(string));
        }

        [TestMethod]
        public void RemoveFormDataFragmentReturnsUpdatedSerializedStringTest()
        {
            var testClass = GetTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);

            var result = dataFragmentService.RemoveFragment(serializedObject, "Name", new KeyValuePair<string, string>());

            Assert.AreNotEqual(serializedObject.Length, result.Length);
        }

        [TestMethod]
        public void RemoveFormDataArrayFragmentReturnsUpdatedSerializedStringTest()
        {
            var testClass = GetTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);

            var result = dataFragmentService.RemoveFragment(serializedObject, "Owners", new KeyValuePair<string, string>());
            Assert.AreNotEqual(serializedObject.Length, result.Length);
        }

        [TestMethod]
        public void RemoveFormDataFilteredArrayFragmentReturnsUpdatedSerializedStringTestTest()
        {
            var testClass = GetTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);
            var fragmentToRemove = testClass.Owners.FirstOrDefault(a => a.Id == 2);
            testClass.Owners.Remove(fragmentToRemove);

            var expectedFragmentserializedObject = serilializedService.SerializeFormData(testClass);
            var filterQuery = new KeyValuePair<string, string>("Id", "2");

            var result = dataFragmentService.RemoveFragment(serializedObject, "Owners", filterQuery);
            Assert.AreEqual(expectedFragmentserializedObject, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void RemoveFormDataFragmentThrowsNotFoundExceptionTest()
        {
            var testClass = GetTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);

            dataFragmentService.RemoveFragment(serializedObject, "InvalidFragmentName", new KeyValuePair<string, string>());
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void RemoveFormDataFragmentFilteredThrowsNotFoundExceptionTest()
        {
            var testClass = GetTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);
            var filterQuery = new KeyValuePair<string, string>("Id", "3");

            dataFragmentService.RemoveFragment(serializedObject, "Owners", filterQuery);
        }

        [TestMethod]
        [ExpectedException(typeof(UnsupportedOperationException))]
        public void RemoveFormDataFragmentInvalidJsonThrowsUnsupportedOperationExceptionTest()
        {
            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            const string serializedObject = "<xml>Not Json</xml>";

            dataFragmentService.RemoveFragment(serializedObject, "Owners", new KeyValuePair<string, string>());
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
