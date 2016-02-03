using System;
using System.Threading.Tasks;
using IPO.PersistenceServices.Forms.Api.Client.Interfaces;
using IPO.PersistenceServices.RestClient.Interfaces;
using IPO.PersistenceServices.SerializationService.Interfaces;

namespace IPO.PersistenceServices.Forms.Api.Client
{
    public class FormsPersistenceClient : IFormsPersistenceClient
    {
        private readonly IRestClient _client;
        private readonly ISerializationService _serializationService;

        private const string BasePath = "Api/V1/FormData";

        public FormsPersistenceClient(IRestClient client, ISerializationService serializationService)
        {
            _client = client;
            _serializationService = serializationService;
        }

        public async Task<T> GetFormDataAsync<T>(Guid formDataId)
        {
            var path = string.Format("{0}/{1}", BasePath, formDataId);
            var serializedFormData = await _client.GetItemAsync<string>(path);

            return _serializationService.DeSerializeFormData<T>(serializedFormData);
        }

        public async Task<T> GetFormDataFragmentAsync<T>(Guid formDataId, string fragmentQuery)
        {
            var path = string.Format("{0}/{1}/Json/Fragment/{2}", BasePath, formDataId, fragmentQuery);
            var serializedFormData = await _client.GetItemAsync<string>(path);

            return _serializationService.DeSerializeFormData<T>(serializedFormData);
        }

        public async Task SaveFormDataAsync<T>(Guid formDataId, T formDataModel)
        {
            var path = string.Format("{0}/{1}", BasePath, formDataId);
            var serializedFormData = _serializationService.SerializeFormData(formDataModel);

            await _client.PostItemAsync(path, serializedFormData);
        }

        public async Task SaveFormDataFragmentAsync<T>(Guid formDataId, T fragmentDataModel, string fragmentQuery)
        {
            var path = string.Format("{0}/{1}/Json/Fragment/{2}", BasePath, formDataId, fragmentQuery);
            var serializedFragmentData = _serializationService.SerializeFormData(fragmentDataModel);

            await _client.PostItemAsync(path, serializedFragmentData);
        }

        public async Task RemoveFormDataAsync(Guid formDataId)
        {
            var path = string.Format("{0}/{1}", BasePath, formDataId);

            await _client.DeleteItemAsync(path);
        }

        public async Task RemoveFormDataFragmentAsync(Guid formDataId, string fragmentQuery)
        {
            var path = string.Format("{0}/{1}/Json/Fragment/{2}", BasePath, formDataId, fragmentQuery);

            await _client.DeleteItemAsync(path);
        }
    }
}