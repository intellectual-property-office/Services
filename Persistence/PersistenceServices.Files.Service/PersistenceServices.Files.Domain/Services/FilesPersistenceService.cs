using System;
using System.Threading.Tasks;
using PersistenceServices.Files.Domain.Entities;
using PersistenceServices.Files.Domain.Exceptions;
using PersistenceServices.Files.Domain.Interfaces;
using IPO.PersistenceServices.Files.Models;

namespace PersistenceServices.Files.Domain.Services
{
    public class FilesPersistenceService : IFilesPersistenceService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILoggerService _loggingService;

        public FilesPersistenceService(IUnitOfWork unitOfWork, ILoggerService loggingService)
        {
            _unitOfWork = unitOfWork;

            _loggingService = loggingService;
        }

        public async Task<FilePersistenceServiceResponseDto> GetFileDataAsync(Guid fileDataId)
        {
            var fileBlobEntity = await _unitOfWork.Repository<FileBlob>().GetAsync(fb => fb.BlobGuid == fileDataId);

            if (fileBlobEntity == null)
            {
                throw new NotFoundException(string.Format("File data not found for id {0}", fileDataId));
            }

            _loggingService.Info(string.Format("Retrieved file (Id:{0}) from persistence store", fileDataId));

            return new FilePersistenceServiceResponseDto
            {
                Success = true, 
                Bytes = fileBlobEntity.Bytes, 
                ContentType = fileBlobEntity.ContentType,  
                FileName = fileBlobEntity.FileName,
                Guid = fileBlobEntity.BlobGuid
            };
        }

        public async Task DeleteFileDataAsync(Guid fileDataId)
        {
            var fileBlobEntity = await _unitOfWork.Repository<FileBlob>().GetAsync(fb => fb.BlobGuid == fileDataId);

            if (fileBlobEntity == null)
            {
                throw new NotFoundException(string.Format("File data not found for id {0}", fileDataId));
            }

            _unitOfWork.Repository<FileBlob>().Remove(fileBlobEntity);
            await _unitOfWork.SaveChanges();

            _loggingService.Info(string.Format("Deleted file (Id:{0}) from persistence store", fileDataId));
        }

        public async Task<Guid> SaveFileDataAsync(byte[] bytes, string contentType, string fileName)
        {
            var fileBlobEntity = new FileBlob(bytes, contentType, fileName);

            _unitOfWork.Repository<FileBlob>().Add(fileBlobEntity);

            await _unitOfWork.SaveChanges();

            _loggingService.Info(string.Format("Saved file {0} to persistence store", fileName));

            return fileBlobEntity.BlobGuid;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}