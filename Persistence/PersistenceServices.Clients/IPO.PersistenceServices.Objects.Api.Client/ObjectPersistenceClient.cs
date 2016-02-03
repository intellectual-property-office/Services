using System.Threading.Tasks;
using IPO.PersistenceServices.Objects.Api.Client.Interfaces;
using IPO.PersistenceServices.RestClient.Interfaces;
using IPO.PersistenceServices.SerializationService.Interfaces;

namespace IPO.PersistenceServices.Objects.Api.Client
{
    public class ObjectPersistenceClient : IObjectPersistenceClient
    {
        private readonly IRestClient _client;
        private readonly ISerializationService _serializationService;

        private const string BasePath = "api/v1/objects";

        public ObjectPersistenceClient(IRestClient client, ISerializationService serializationService)
        {
            _client = client;
            _serializationService = serializationService;
        }

        public async Task<T> GetObjectDataAsync<T>(string key)
        {
            var path = string.Format("{0}/{1}", BasePath, key);
            var serializedData = await _client.GetItemAsync<string>(path);

            return _serializationService.DeSerializeFormData<T>(serializedData);
        }

        public async Task SaveObjectDataAsync<T>(string key, T objectDataModel)
        {
            var path = string.Format("{0}/{1}", BasePath, key);
            var serializedData = _serializationService.SerializeFormData(objectDataModel);

            await _client.PostItemAsync(path, serializedData);
        }

        public async Task RemoveObjectDataAsync(string key)
        {
            var path = string.Format("{0}/{1}", BasePath, key);

            await _client.DeleteItemAsync(path);
        }
    }
}