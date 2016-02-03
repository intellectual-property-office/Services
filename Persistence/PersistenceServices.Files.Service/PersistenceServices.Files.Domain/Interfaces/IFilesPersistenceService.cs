using System;
using System.Threading.Tasks;
using IPO.PersistenceServices.Files.Models;

namespace PersistenceServices.Files.Domain.Interfaces
{
    public interface IFilesPersistenceService : IDisposable
    {
        Task<Guid> SaveFileDataAsync(byte[] bytes, string contentType, string fileName);

        Task<FilePersistenceServiceResponseDto> GetFileDataAsync(Guid fileDataId);

        Task DeleteFileDataAsync(Guid fileDataId);
    }
}