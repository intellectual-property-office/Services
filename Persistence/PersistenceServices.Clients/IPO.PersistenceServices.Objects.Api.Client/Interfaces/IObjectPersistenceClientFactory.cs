namespace IPO.PersistenceServices.Objects.Api.Client.Interfaces
{
    public interface IObjectPersistenceClientFactory
    {
        IObjectPersistenceClient ClientInstance { get; }
    }
}