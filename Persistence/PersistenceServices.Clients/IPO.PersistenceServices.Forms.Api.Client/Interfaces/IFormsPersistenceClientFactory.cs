namespace IPO.PersistenceServices.Forms.Api.Client.Interfaces
{
    public interface IFormsPersistenceClientFactory
    {
        IFormsPersistenceClient ClientInstance { get; }
    }
}