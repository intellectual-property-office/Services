using System;
using System.Threading.Tasks;

namespace PersistenceServices.Forms.Domain.Interfaces
{
    public interface IFormsPersistenceService : IDisposable
    {
        Task<string> GetFormDataAsync(Guid formDataId);

        Task SaveFormDataAsync(Guid formDataId, string formDataDto);

        Task RemoveFormDataAsync(Guid formDataId);
    }
}