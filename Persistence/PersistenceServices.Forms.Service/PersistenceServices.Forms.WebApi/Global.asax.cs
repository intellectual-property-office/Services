using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using PersistenceServices.Forms.WebApi.Framework;

namespace PersistenceServices.Forms.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            UnityConfig.RegisterComponents();
        }
    }
}