using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersistenceServices.Forms.Domain.Exceptions;

namespace PersistenceServices.Forms.Domain.Tests.ExceptionTests
{
    [TestClass]
    public class UnsupportedOperationExceptionTests
    {
        [TestMethod]
        public void UnsupportedOperationExceptionHasBaseClassOfException()
        {
            var superClass = typeof(UnsupportedOperationException).BaseType;
            Assert.AreSame(superClass, typeof(Exception));
        }

        [TestMethod]
        public void UnsupportedOperationExceptionPassesMessageIntoBaseClassOfException()
        {
            var notFoundException = new UnsupportedOperationException("Unsupported exception");
            var result = notFoundException.GetBaseException();

            Assert.AreEqual("Unsupported exception", result.Message);
        }

        [TestMethod]
        public void UnsupportedOperationExceptionExtendedConstructorPassesMessageIntoBaseClassOfException()
        {
            var notFoundException = new UnsupportedOperationException("Unsupported exception", new Exception("Inner Exception"));
            var result = notFoundException.GetBaseException();

            Assert.AreEqual("Inner Exception", result.GetBaseException().Message);
        }
    }
}