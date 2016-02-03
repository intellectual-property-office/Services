using System.Web.Http;
using PersistenceServices.Forms.WebApi.Framework;

namespace PersistenceServices.Forms.WebApi
{
    public class FilterConfig
    {
        public static void RegisterFilters()
        {
            GlobalConfiguration.Configuration.Filters.Add(new ErrorLoggingFilterAttribute());
        }
    }
}