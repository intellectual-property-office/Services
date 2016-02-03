using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PersistenceServices.Forms.Domain.Interfaces;

namespace PersistenceServices.Forms.WebApi.Controllers
{
    [RoutePrefix("Api/V1/FormData")]
    public class FormsPersistenceFragmentController : ApiController
    {
        private readonly IFormsPersistenceFragmentService _formsPersistenceFragmentService;

        public FormsPersistenceFragmentController(IFormsPersistenceFragmentService formsPersistenceFragmentService)
        {
            _formsPersistenceFragmentService = formsPersistenceFragmentService;
        }

        [HttpGet]
        [Route("{formDataId}/Json/Fragment/{fragmentName}")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> GetFormDataFragmentAsync(Guid formDataId, string fragmentName)
        {
            var fragmentFilter = GetFragmentFilter();
            var serializedFragmentData = await _formsPersistenceFragmentService.GetFormDataFragmentAsync(formDataId, fragmentName, fragmentFilter);

            return Ok(serializedFragmentData);
        }

        [HttpPost]
        [Route("{formDataId}/Json/Fragment/{fragmentName}")]
        public async Task<IHttpActionResult> SaveFormDataFragmentAsync(Guid formDataId, string fragmentName, [FromBody]string serializedFragmentData)
        {
            var fragmentFilter = GetFragmentFilter();
            await _formsPersistenceFragmentService.SaveFormDataFragmentAsync(formDataId, fragmentName, fragmentFilter, serializedFragmentData);

            return Ok();
        }

        [HttpDelete]
        [Route("{formDataId}/Json/Fragment/{fragmentName}")]
        public async Task<IHttpActionResult> RemoveFormFragmentDataAsync(Guid formDataId, string fragmentName)
        {
            var fragmentFilter = GetFragmentFilter();
            await _formsPersistenceFragmentService.RemoveFormDataFragmentAsync(formDataId, fragmentName, fragmentFilter);

            return Ok();
        }

        public KeyValuePair<string, string> GetFragmentFilter()
        {
            var fragmentFilter = new KeyValuePair<string, string>();

            if (Request != null)
            {
                fragmentFilter = Request.GetQueryNameValuePairs().FirstOrDefault();
            }

            return fragmentFilter;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _formsPersistenceFragmentService.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}