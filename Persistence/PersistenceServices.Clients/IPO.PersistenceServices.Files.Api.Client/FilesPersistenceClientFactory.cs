using System;
using System.Net.Http;
using IPO.PersistenceServices.RestClient.Interfaces;
using Microsoft.Practices.Unity;
using IPO.PersistenceServices.Files.Api.Client.Interfaces;

namespace IPO.PersistenceServices.Files.Api.Client
{
    public class FilesPersistenceClientFactory : IFilesPersistenceClientFactory
    {
        private readonly UnityContainer _unityContainer;

        private string _filesPersistenceWebServiceAddress;

        public FilesPersistenceClientFactory(string filesPersistenceWebServiceAddress)
        {
            _unityContainer = new UnityContainer();
            _filesPersistenceWebServiceAddress = filesPersistenceWebServiceAddress;

        }

        public IFilesPersistenceClient GetClientInstance()
        {
            RegisterClientComponents(_filesPersistenceWebServiceAddress);
            return _unityContainer.Resolve<IFilesPersistenceClient>();
        }

        private void RegisterClientComponents(string formsPersistenceWebServiceAddress)
        {

            _unityContainer.RegisterType<HttpClient>("httpRepository",
                new InjectionFactory(x => new HttpClient {BaseAddress = new Uri(formsPersistenceWebServiceAddress)}));

            _unityContainer.RegisterType<IRestClient, RestClient.RestClient>("httpRepositoryClient",
                new HierarchicalLifetimeManager(),
                new InjectionFactory(x => new RestClient.RestClient(x.Resolve<HttpClient>(("httpRepository")))));

            _unityContainer.RegisterType<IFilesPersistenceClient, FilesPersistenceClient>(
                new HierarchicalLifetimeManager(),
                new InjectionFactory(x => new FilesPersistenceClient(x.Resolve<IRestClient>("httpRepositoryClient"))));
        }
    }
}