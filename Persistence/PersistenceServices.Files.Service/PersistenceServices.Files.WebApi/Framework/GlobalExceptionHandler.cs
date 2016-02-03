using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using PersistenceServices.Files.Domain.Exceptions;

namespace PersistenceServices.Files.WebApi.Framework
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is NotFoundException)
            {
                context.Result = new ExceptionResponse(HttpStatusCode.NotFound, context.Exception.Message, context.ExceptionContext.Request);
                return;
            }

            if (context.Exception is UnsupportedOperationException)
            {
                context.Result = new ExceptionResponse(HttpStatusCode.BadRequest, context.Exception.Message, context.ExceptionContext.Request);
                return;
            }

            context.Result = new ExceptionResponse(
                HttpStatusCode.BadRequest, string.Format("Error occurred during API call - {0}", context.Exception.Message), context.ExceptionContext.Request);
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return context.CatchBlock.IsTopLevel;
        }

        private class ExceptionResponse : IHttpActionResult
        {
            public ExceptionResponse(HttpStatusCode statusCode, string message, HttpRequestMessage request)
            {
                StatusCode = statusCode;
                Message = message;
                Request = request;
            }

            private HttpStatusCode StatusCode { get; set; }
            private string Message { get; set; }
            private HttpRequestMessage Request { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(StatusCode)
                {
                    Content = new StringContent(Message),
                    RequestMessage = Request
                };

                return Task.FromResult(response);
            }
        }
    }
}