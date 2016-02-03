using IPO.PersistenceServices.SerializationService.Interfaces;
using Newtonsoft.Json;

namespace IPO.PersistenceServices.SerializationService
{
    public class JsonSerializationService : ISerializationService
    {
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public string SerializeFormData<T>(T objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize, _serializerSettings);
        }

        public T DeSerializeFormData<T>(string stringToDeSerialize)
        {
            return JsonConvert.DeserializeObject<T>(stringToDeSerialize, _serializerSettings);
        }

        public object DeSerializeFormData(string stringToDeSerialize)
        {
            return JsonConvert.DeserializeObject(stringToDeSerialize, _serializerSettings);
        }
    }
}