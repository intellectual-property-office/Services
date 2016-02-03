using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using PersistenceServices.Files.WebApi.Framework;

namespace PersistenceServices.Files.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            UnityConfig.RegisterComponents();

            FilterConfig.RegisterFilters();
        }
    }
}