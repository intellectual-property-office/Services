using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PersistenceServices.Forms.Domain.Interfaces;

namespace PersistenceServices.Forms.WebApi.Controllers
{
    [RoutePrefix("Api/V1/FormData")]
    public class FormsPersistenceController : ApiController
    {
        private readonly IFormsPersistenceService _formsPersistenceService;

        public FormsPersistenceController(IFormsPersistenceService formsPersistenceService)
        {
            _formsPersistenceService = formsPersistenceService;
        }

        [HttpGet]
        [Route("{formDataId}")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> GetFormDataAsync(Guid formDataId)
        {
            var serializedFormData = await _formsPersistenceService.GetFormDataAsync(formDataId);

            return Ok(serializedFormData);
        }

        [HttpPost]
        [Route("{formDataId}")]
        public async Task<IHttpActionResult> SaveFormDataAsync(Guid formDataId, [FromBody]string serializedFormData)
        {
            await _formsPersistenceService.SaveFormDataAsync(formDataId, serializedFormData);

            return Ok();
        }

        [HttpDelete]
        [Route("{formDataId}")]
        public async Task<IHttpActionResult> RemoveFormDataAsync(Guid formDataId)
        {
            await _formsPersistenceService.RemoveFormDataAsync(formDataId);

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _formsPersistenceService.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}