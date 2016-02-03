using System;
using System.Net.Http;
using IPO.PersistenceServices.Objects.Api.Client.Interfaces;
using IPO.PersistenceServices.RestClient.Interfaces;
using IPO.PersistenceServices.SerializationService;
using IPO.PersistenceServices.SerializationService.Interfaces;
using Microsoft.Practices.Unity;

namespace IPO.PersistenceServices.Objects.Api.Client
{
    public class ObjectPersistenceClientFactory : IObjectPersistenceClientFactory
    {
        private readonly IObjectPersistenceClient _clientInstance;
        public IObjectPersistenceClient ClientInstance { get { return _clientInstance; } }

        public ObjectPersistenceClientFactory(string webServiceAddress)
        {
            _clientInstance = GetClientInstance(webServiceAddress);
        }

        private static IObjectPersistenceClient GetClientInstance(string webServiceAddress)
        {
            UnityContainer unityContainer = new UnityContainer();

            unityContainer.RegisterType<ISerializationService, JsonSerializationService>(new TransientLifetimeManager());

            unityContainer.RegisterType<HttpClient>("httpRepository",
                new HierarchicalLifetimeManager(),
                new InjectionFactory(x => new HttpClient { BaseAddress = new Uri(webServiceAddress) }));

            unityContainer.RegisterType<IRestClient, RestClient.RestClient>("httpRepositoryClient",
                new HierarchicalLifetimeManager(),
                new InjectionFactory(x => new RestClient.RestClient(x.Resolve<HttpClient>(("httpRepository")))));

            unityContainer.RegisterType<IObjectPersistenceClient, ObjectPersistenceClient>(
                new HierarchicalLifetimeManager(),
                new InjectionFactory(x => new ObjectPersistenceClient(x.Resolve<IRestClient>("httpRepositoryClient"), x.Resolve<ISerializationService>())));

            return unityContainer.Resolve<IObjectPersistenceClient>();
        }
    }
}