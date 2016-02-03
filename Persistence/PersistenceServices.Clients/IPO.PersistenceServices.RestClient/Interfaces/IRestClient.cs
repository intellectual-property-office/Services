using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPO.PersistenceServices.RestClient.Interfaces
{
    public interface IRestClient
    {
        Task<IEnumerable<T>> GetAsync<T>(string requestUri);

        Task<T> GetItemAsync<T>(string requestUri);

        Task PostItemAsync<T>(string requestUri, T item);

        Task<T2> PostItemAsync<T1, T2>(string requestUri, T1 item);

        Task PutItem<T>(string requestUri, T item);

        Task DeleteItemAsync(string requestUri);
    }
}