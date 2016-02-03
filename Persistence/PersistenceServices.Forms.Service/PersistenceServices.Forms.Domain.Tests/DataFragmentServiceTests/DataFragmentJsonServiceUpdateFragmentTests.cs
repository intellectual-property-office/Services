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
    public class DataFragmentJsonServiceUpdateFragmentTests
    {
        [TestMethod]
        public void UpdateFormDataFragmentReturnsSerializedStringTest()
        {
            var testClass = GetTestClass();
            var updatedTestClass = GetUpdatedTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);
            var serializedFragmentObject = serilializedService.SerializeFormData(updatedTestClass.Owners);

            var result = dataFragmentService.UpdateFragment(serializedObject, serializedFragmentObject, "Owners", new KeyValuePair<string, string>());
            Assert.IsInstanceOfType(result, typeof(string));
        }

        [TestMethod]
        public void UpdateFormDataFragmentReturnsUpdatedSerializedStringTest()
        {
            var testClass = GetTestClass();
            var updatedTestClass = GetUpdatedTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);
            var serializedFragmentObject = serilializedService.SerializeFormData(updatedTestClass.Name);

            testClass.Name = updatedTestClass.Name;
            var expectedSerializedObject = serilializedService.SerializeFormData(testClass);

            var result = dataFragmentService.UpdateFragment(serializedObject, serializedFragmentObject, "Name", new KeyValuePair<string, string>());
            Assert.AreEqual(expectedSerializedObject, result);
        }

        [TestMethod]
        public void UpdateFormDataArrayFragmentReturnsUpdatedSerializedStringTest()
        {
            var testClass = GetTestClass();
            var updatedTestClass = GetUpdatedTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);
            var serializedFragmentObject = serilializedService.SerializeFormData(updatedTestClass.Owners);

            testClass.Owners = updatedTestClass.Owners;
            var expectedSerializedObject = serilializedService.SerializeFormData(testClass);

            var result = dataFragmentService.UpdateFragment(serializedObject, serializedFragmentObject, "Owners", new KeyValuePair<string, string>());
            Assert.AreEqual(expectedSerializedObject, result);
        }

        [TestMethod]
        public void UpdateFormDataFilteredArrayFragmentReturnsUpdatedSerializedStringTest()
        {
            var testClass = GetTestClass();
            var updatedTestClass = GetUpdatedTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);

            var fragmentToUpdate = testClass.Owners.FirstOrDefault(o => o.Id == 2);
            testClass.Owners.Remove(fragmentToUpdate);
            fragmentToUpdate = updatedTestClass.Owners.FirstOrDefault(o => o.Id == 3);
            testClass.Owners.Add(fragmentToUpdate);
            var serializedFragmentObject = serilializedService.SerializeFormData(fragmentToUpdate);

            var expectedSerializedObject = serilializedService.SerializeFormData(testClass);

            var filterQuery = new KeyValuePair<string, string>("Id", "2");
            var result = dataFragmentService.UpdateFragment(serializedObject, serializedFragmentObject, "Owners", filterQuery);
            Assert.AreEqual(expectedSerializedObject, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void UpdateFormDataFragmentUpdateFragmentThrowsNotFoundExceptionTest()
        {
            var testClass = GetTestClass();
            var updatedTestClass = GetUpdatedTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);
            var serializedFragmentObject = serilializedService.SerializeFormData(updatedTestClass.Owners);

            dataFragmentService.UpdateFragment(serializedObject, serializedFragmentObject, "InvalidFragmentName", new KeyValuePair<string, string>());
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void UpdateFormDataFragmentUpdateFilteredFragmentThrowsNotFoundExceptionTest()
        {
            var testClass = GetTestClass();
            var updatedTestClass = GetUpdatedTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);
            var serializedFragmentObject = serilializedService.SerializeFormData(updatedTestClass.Owners);
            var filterQuery = new KeyValuePair<string, string>("Id", "6");

            dataFragmentService.UpdateFragment(serializedObject, serializedFragmentObject, "Owners", filterQuery);
        }

        [TestMethod]
        [ExpectedException(typeof(UnsupportedOperationException))]
        public void UpdateFormDataFragmentInvalidJsonThrowsUnsupportedOperationExceptionTest()
        {
            var testClass = GetTestClass();

            var serilializedService = new JsonSerializationService();
            var dataFragmentService = new DataFragmentJsonService(serilializedService);
            var serializedObject = serilializedService.SerializeFormData(testClass);
            const string serializedFragmentObject = "<xml>Not Json</xml>";

            dataFragmentService.UpdateFragment(serializedObject, serializedFragmentObject, "Owners", new KeyValuePair<string, string>());
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

        private static TestSerializableClass GetUpdatedTestClass()
        {
            return new TestSerializableClass
            {
                Id = 1,

                Name = "TestClass",

                Owners = new List<TestSerializableOwnerClass>
                {
                    new TestSerializableOwnerClass
                    {
                        Id = 3,
                        Address = new TestSerializableAddressClass
                        {
                            Id = 3,
                            Addressline1 = "Updated Test address 3",
                            Town = "Updated Test town 3",
                            Postcode = "Updated Test postcode 3"
                        }
                    },

                    new TestSerializableOwnerClass
                    {
                        Id = 4,
                        Address = new TestSerializableAddressClass
                        {
                            Id = 4,
                            Addressline1 = "Updated Test address 4",
                            Town = "Updated Test town 4",
                            Postcode = "Updated Test postcode 4"
                        }
                    }
                }
            };

        }
    }
}
