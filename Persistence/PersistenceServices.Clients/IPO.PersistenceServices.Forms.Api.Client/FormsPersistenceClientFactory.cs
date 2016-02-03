using System;
using System.Net.Http;
using IPO.PersistenceServices.Forms.Api.Client.Interfaces;
using IPO.PersistenceServices.RestClient.Interfaces;
using IPO.PersistenceServices.SerializationService;
using IPO.PersistenceServices.SerializationService.Interfaces;
using Microsoft.Practices.Unity;

namespace IPO.PersistenceServices.Forms.Api.Client
{
    public class FormsPersistenceClientFactory : IFormsPersistenceClientFactory
    {
        private readonly IFormsPersistenceClient _clientInstance;
        public IFormsPersistenceClient ClientInstance { get { return _clientInstance; } }

        public FormsPersistenceClientFactory(string webServiceAddress)
        {
            _clientInstance = GetClientInstance(webServiceAddress);
        }

        private static IFormsPersistenceClient GetClientInstance(string webServiceAddress)
        {
            UnityContainer unityContainer = new UnityContainer();

            unityContainer.RegisterType<ISerializationService, JsonSerializationService>(new TransientLifetimeManager());

            unityContainer.RegisterType<HttpClient>("httpRepository",
                new HierarchicalLifetimeManager(),
                new InjectionFactory(x => new HttpClient { BaseAddress = new Uri(webServiceAddress) }));

            unityContainer.RegisterType<IRestClient, RestClient.RestClient>("httpRepositoryClient",
                new HierarchicalLifetimeManager(),
                new InjectionFactory(x => new RestClient.RestClient(x.Resolve<HttpClient>(("httpRepository")))));

            unityContainer.RegisterType<IFormsPersistenceClient, FormsPersistenceClient>(
                new HierarchicalLifetimeManager(),
                new InjectionFactory(x => new FormsPersistenceClient(x.Resolve<IRestClient>("httpRepositoryClient"), x.Resolve<ISerializationService>())));

            return unityContainer.Resolve<IFormsPersistenceClient>();
        }
    }
}