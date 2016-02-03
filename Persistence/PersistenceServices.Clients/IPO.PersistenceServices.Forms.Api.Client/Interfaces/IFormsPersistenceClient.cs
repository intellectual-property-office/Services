using System;
using System.Threading.Tasks;

namespace IPO.PersistenceServices.Forms.Api.Client.Interfaces
{
    public interface IFormsPersistenceClient
    {
        Task<T> GetFormDataAsync<T>(Guid formDataId);

        Task<T> GetFormDataFragmentAsync<T>(Guid formDataId, string fragmentQuery);

        Task SaveFormDataAsync<T>(Guid formDataId, T formDataModel);

        Task SaveFormDataFragmentAsync<T>(Guid formDataId, T formDataFragmentModel, string fragmentQuery);

        Task RemoveFormDataAsync(Guid formDataId);

        Task RemoveFormDataFragmentAsync(Guid formDataId, string fragmentQuery);
    }
}