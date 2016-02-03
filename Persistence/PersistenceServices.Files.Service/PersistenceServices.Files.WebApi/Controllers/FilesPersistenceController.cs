using System.Web.Http;
using System;
using System.Threading.Tasks;
using System.Web.Http.Description;
using IPO.PersistenceServices.Files.Models;
using PersistenceServices.Files.Domain.Interfaces;

namespace PersistenceServices.Files.WebApi.Controllers
{
    [RoutePrefix("Api/V1/File")]
    public class FilesPersistenceController : ApiController
    {
        private readonly IFilesPersistenceService _filesPersistenceService;

        public FilesPersistenceController(IFilesPersistenceService filesPersistenceService)
        {
            _filesPersistenceService = filesPersistenceService;
        }

        [HttpGet]
        [Route("{fileGuid}")]
        [ResponseType(typeof(FilePersistenceServiceResponseDto))]
        public async Task<IHttpActionResult> GetFileDataAsync(Guid fileGuid)
        {
            var bytes = await _filesPersistenceService.GetFileDataAsync(fileGuid);

            return Ok(bytes);
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(FilePersistenceServiceResponseDto))]
        public async Task<IHttpActionResult> SaveFileDataAsync(FilePersistenceServiceRequestDto fileBlobServiceRequestDto)
        {
            var guid = await _filesPersistenceService.SaveFileDataAsync(fileBlobServiceRequestDto.Bytes, fileBlobServiceRequestDto.ContentType, fileBlobServiceRequestDto.FileName);

            return Ok(new FilePersistenceServiceResponseDto{Guid = guid});
        }

        [HttpDelete]
        [Route("{fileGuid}")]
        public async Task<IHttpActionResult> DeleteFileDataAsync(Guid fileGuid)
        {
            await _filesPersistenceService.DeleteFileDataAsync(fileGuid);

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _filesPersistenceService.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}