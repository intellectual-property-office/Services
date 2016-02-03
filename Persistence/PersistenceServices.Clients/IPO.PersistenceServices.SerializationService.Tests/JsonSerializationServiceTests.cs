using System;
using IPO.PersistenceServices.SerializationService.Interfaces;
using IPO.PersistenceServices.SerializationService.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IPO.PersistenceServices.SerializationService.Tests
{
    [TestClass]
    public class JsonSerializationServiceTests
    {
        [TestMethod]
        public void SerializationServiceSerializeImplementsISerializationServiceInterfaceTest()
        {
            var serializationService = new JsonSerializationService();
            var testImplementsInterface = serializationService as ISerializationService;
            Assert.IsNotNull(testImplementsInterface);
        }

        [TestMethod]
        public void SerializationServiceSerializeObjectSerializesToStringTest()
        {
            var serializationService = new JsonSerializationService();
            var testObject = new SerializationTestClass("Jamie", DateTime.Now);
            var serializedObject = serializationService.SerializeFormData(testObject);
            Assert.IsInstanceOfType(serializedObject, typeof (string));
        }

        [TestMethod]
        public void SerializationServiceDeSerializeStringDeSerializesToObjectTest()
        {
            var serializationService = new JsonSerializationService();
            var testObject = new SerializationTestClass("Jamie", DateTime.Now);
            var serializedObject = serializationService.SerializeFormData(testObject);
            var deserializedObject = serializationService.DeSerializeFormData<SerializationTestClass>(serializedObject);
            Assert.IsInstanceOfType(deserializedObject, typeof (SerializationTestClass));
        }

        [TestMethod]
        public void SerializationServiceDeSerializeStringDeSerializesUsingTypeNameToObjectTest()
        {
            var serializationService = new JsonSerializationService();
            var testObject = new SerializationTestClass("Bob", DateTime.Now);
            var serializedObject = serializationService.SerializeFormData(testObject);
            var deserializedObject = serializationService.DeSerializeFormData(serializedObject);
            Assert.IsInstanceOfType(deserializedObject, typeof (SerializationTestClass));
        }
    }
}