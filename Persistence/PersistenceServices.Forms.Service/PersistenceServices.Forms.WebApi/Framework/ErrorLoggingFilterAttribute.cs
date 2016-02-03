using System.Web.Http;
using System.Web.Http.Filters;
using IPO.Logging;

namespace PersistenceServices.Forms.WebApi.Framework
{
    public class ErrorLoggingFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var logger =
                (ILoggerService)
                    GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILoggerService));

            logger.Error(context.Exception.Message);
        }
    }
}