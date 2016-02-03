using System;
using System.Net;
using System.Threading;
using System.Web.Http.ExceptionHandling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersistenceServices.Forms.Domain.Exceptions;
using PersistenceServices.Forms.WebApi.Framework;

namespace PersistenceServices.Forms.WebApi.Tests
{
    [TestClass]
    public class GlobalExceptionHandlerTests
    {
        [TestMethod]
        public void FormsPersistenceControllerReturnsNotFoundHttpResponseExceptionForNotFoundException()
        {
            var exceptionContext = new ExceptionContext(new NotFoundException("Not found error"), new ExceptionContextCatchBlock("test", true, true));
            var exceptionHandlerContext = new ExceptionHandlerContext(exceptionContext);
            var globalExceptionHandler = new GlobalExceptionHandler();

            globalExceptionHandler.Handle(exceptionHandlerContext);
            var result = exceptionHandlerContext.Result.ExecuteAsync(new CancellationToken()).Result;
            
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
            Assert.AreEqual("Not found error", result.Content.ReadAsStringAsync().Result);
        }

        [TestMethod]
        public void FormsPersistenceControllerReturnsBadRequestHttpResponseExceptionForUnsupportedOperationException()
        {
            var exceptionContext = new ExceptionContext(new UnsupportedOperationException("Unsupported operation"), new ExceptionContextCatchBlock("test", true, true));
            var exceptionHandlerContext = new ExceptionHandlerContext(exceptionContext);
            var globalExceptionHandler = new GlobalExceptionHandler();

            globalExceptionHandler.Handle(exceptionHandlerContext);
            var result = exceptionHandlerContext.Result.ExecuteAsync(new CancellationToken()).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual("Unsupported operation", result.Content.ReadAsStringAsync().Result);
        }

        [TestMethod]
        public void FormsPersistenceControllerReturnsBadRequestHttpResponseExceptionForGeneralException()
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