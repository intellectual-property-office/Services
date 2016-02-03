using System.Web.Http;
using PersistenceServices.Files.WebApi.Framework;

namespace PersistenceServices.Files.WebApi
{
    public class FilterConfig
    {
        public static void RegisterFilters()
        {
            GlobalConfiguration.Configuration.Filters.Add(new ErrorLoggingFilterAttribute());
        }
    }
}