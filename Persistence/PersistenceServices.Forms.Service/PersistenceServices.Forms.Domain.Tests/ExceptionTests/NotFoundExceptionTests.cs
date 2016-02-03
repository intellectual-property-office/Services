using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersistenceServices.Forms.Domain.Exceptions;

namespace PersistenceServices.Forms.Domain.Tests.ExceptionTests
{
    [TestClass]
    public class NotFoundExceptionTests
    {
        [TestMethod]
        public void NotFoundExceptionHasBaseClassOfException()
        {
            var superClass = typeof(NotFoundException).BaseType;
            Assert.AreSame(superClass, typeof(Exception));
        }

        [TestMethod]
        public void NotFoundExceptionPassesMessageIntoBaseClassOfException()
        {
            var notFoundException = new NotFoundException("Not found");
            var result = notFoundException.GetBaseException();

            Assert.AreEqual("Not found", result.Message);
        }

        [TestMethod]
        public void NotFoundExceptionExtendedConstructorPassesMessageIntoBaseClassOfException()
        {
            var notFoundException = new NotFoundException("Not found", new Exception("Inner Exception"));
            var result = notFoundException.GetBaseException();

            Assert.AreEqual("Inner Exception", result.GetBaseException().Message);
        }
    }
}