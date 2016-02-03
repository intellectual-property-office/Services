namespace IPO.PersistenceServices.Files.Api.Client.Interfaces
{
    public interface IFilesPersistenceClientFactory
    {
        IFilesPersistenceClient GetClientInstance();
    }
}