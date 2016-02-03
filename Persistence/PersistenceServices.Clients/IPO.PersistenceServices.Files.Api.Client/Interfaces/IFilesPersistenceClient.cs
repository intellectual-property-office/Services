using System;
using System.Threading.Tasks;

namespace IPO.PersistenceServices.Files.Api.Client.Interfaces
{
    public interface IFilesPersistenceClient
    {
        Task<T2> SaveFileDataAsync<T1, T2>(T1 fileDataModel);

        Task<T> GetFileDataAsync<T>(Guid fileGuid);

        Task RemoveFileDataAsync(Guid fileGuid);
    }
}