namespace IPO.PersistenceServices.SerializationService.Interfaces
{
    public interface ISerializationService
    {
        string SerializeFormData<T>(T fromDataModel);

        T DeSerializeFormData<T>(string serializedFormData);

        object DeSerializeFormData(string stringToDeSerialize);
    }
}