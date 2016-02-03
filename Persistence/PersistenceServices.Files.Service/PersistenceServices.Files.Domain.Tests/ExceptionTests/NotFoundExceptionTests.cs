using System;
using NUnit.Framework;
using PersistenceServices.Files.Domain.Exceptions;

namespace PersistenceServices.Files.Domain.Tests.ExceptionTests
{
    public class NotFoundExceptionTests
    {
        [Test]
        public void NotFoundExceptionHasBaseClassOfException()
        {
            var superClass = typeof(NotFoundException).BaseType;
            Assert.AreSame(superClass, typeof(Exception));
        }

        [Test]
        public void NotFoundExceptionPassesMessageIntoBaseClassOfException()
        {
            var notFoundException = new NotFoundException("Not found");
            var result = notFoundException.GetBaseException();

            Assert.AreEqual("Not found", result.Message);
        }

        [Test]
        public void NotFoundExceptionExtendedConstructorPassesMessageIntoBaseClassOfException()
        {
            var notFoundException = new NotFoundException("Not found", new Exception("Inner Exception"));
            var result = notFoundException.GetBaseException();

            Assert.AreEqual("Inner Exception", result.GetBaseException().Message);
        }
    }
}