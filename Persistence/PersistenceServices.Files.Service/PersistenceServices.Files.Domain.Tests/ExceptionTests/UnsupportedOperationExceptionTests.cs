using System;
using NUnit.Framework;
using PersistenceServices.Files.Domain.Exceptions;

namespace PersistenceServices.Files.Domain.Tests.ExceptionTests
{
    public class UnsupportedOperationExceptionTests
    {
        [Test]
        public void UnsupportedOperationExceptionHasBaseClassOfException()
        {
            var superClass = typeof(UnsupportedOperationException).BaseType;
            Assert.AreSame(superClass, typeof(Exception));
        }

        [Test]
        public void UnsupportedOperationExceptionPassesMessageIntoBaseClassOfException()
        {
            var notFoundException = new UnsupportedOperationException("Unsupported exception");
            var result = notFoundException.GetBaseException();

            Assert.AreEqual("Unsupported exception", result.Message);
        }

        [Test]
        public void UnsupportedOperationExceptionExtendedConstructorPassesMessageIntoBaseClassOfException()
        {
            var notFoundException = new UnsupportedOperationException("Unsupported exception", new Exception("Inner Exception"));
            var result = notFoundException.GetBaseException();

            Assert.AreEqual("Inner Exception", result.GetBaseException().Message);
        }
    }
}