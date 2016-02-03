using System;
using System.Net;
using System.Threading;
using System.Web.Http.ExceptionHandling;
using NUnit.Framework;
using PersistenceServices.Files.Domain.Exceptions;
using PersistenceServices.Files.WebApi.Framework;

namespace PersistenceServices.Files.WebApi.Tests
{
    [TestFixture]
    public class GlobalExceptionHandlerTests
    {
        [Test]
        public void FilesPersistenceControllerReturnsNotFoundHttpResponseExceptionForNotFoundException()
        {
            var exceptionContext = new ExceptionContext(new NotFoundException("Not found error"), new ExceptionContextCatchBlock("test", true, true));
            var exceptionHandlerContext = new ExceptionHandlerContext(exceptionContext);
            var globalExceptionHandler = new GlobalExceptionHandler();

            globalExceptionHandler.Handle(exceptionHandlerContext);
            var result = exceptionHandlerContext.Result.ExecuteAsync(new CancellationToken()).Result;
            
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
            Assert.AreEqual("Not found error", result.Content.ReadAsStringAsync().Result);
        }

        [Test]
        public void FilesPersistenceControllerReturnsBadRequestHttpResponseExceptionForUnsupportedOperationException()
        {
            var exceptionContext = new ExceptionContext(new UnsupportedOperationException("Unsupported operation"), new ExceptionContextCatchBlock("test", true, true));
            var exceptionHandlerContext = new ExceptionHandlerContext(exceptionContext);
            var globalExceptionHandler = new GlobalExceptionHandler();

            globalExceptionHandler.Handle(exceptionHandlerContext);
            var result = exceptionHandlerContext.Result.ExecuteAsync(new CancellationToken()).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual("Unsupported operation", result.Content.ReadAsStringAsync().Result);
        }

        [Test]
        public void FilesPersistenceControllerReturnsBadRequestHttpResponseExceptionForGeneralException()
        {
            var exceptionContext = new ExceptionContext(new Exception("General error"), new ExceptionContextCatchBlock("test", true, true));
            var exceptionHandlerContext = new ExceptionHandlerContext(exceptionContext);
            var globalExceptionHandler = new GlobalExceptionHandler();

            globalExceptionHandler.Handle(exceptionHandlerContext);
            var result = exceptionHandlerContext.Result.ExecuteAsync(new CancellationToken()).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual("Error occurred during API call - General error", result.Content.ReadAsStringAsync().Result);
        }
    }
}