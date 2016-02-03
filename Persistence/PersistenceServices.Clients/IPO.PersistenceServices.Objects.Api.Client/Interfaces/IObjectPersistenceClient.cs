using System.Threading.Tasks;

namespace IPO.PersistenceServices.Objects.Api.Client.Interfaces
{
    public interface IObjectPersistenceClient
    {
        Task<T> GetObjectDataAsync<T>(string key);
        Task SaveObjectDataAsync<T>(string key, T objectDataModel);
        Task RemoveObjectDataAsync(string key);
    }
}