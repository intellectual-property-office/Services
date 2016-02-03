using System;
using System.Net.Http;
using Microsoft.Practices.Unity;
using IPO.PersistenceServices.RestClient.Interfaces;
using IPO.PersistenceServices.Files.Api.Client.Interfaces;

namespace IPO.PersistenceServices.Files.Api.Client.Framework
{
    public static class FilesPersistenceClientFactoryOld
    {
        private static readonly UnityContainer UnityContainer = new UnityContainer();

        public static IFilesPersistenceClient GetClientInstance(string filesPersistenceWebServiceAddress)
        {
            RegisterClientComponents(filesPersistenceWebServiceAddress);
            return UnityContainer.Resolve<IFilesPersistenceClient>();
        }

        private static void RegisterClientComponents(string filesPersistenceWebServiceAddress)
        {
            UnityContainer.RegisterType<HttpClient>("httpRepository",
                new InjectionFactory(x => new HttpClient { BaseAddress = new Uri(filesPersistenceWebServiceAddress) }));

            UnityContainer.RegisterType<IRestClient, RestClient.RestClient>("httpRepositoryClient",
                new HierarchicalLifetimeManager(),
                new InjectionFactory(x => new RestClient.RestClient(x.Resolve<HttpClient>(("httpRepository")))));

            UnityContainer.RegisterType<IFilesPersistenceClient, FilesPersistenceClient>(
                          new HierarchicalLifetimeManager(),
                          new InjectionFactory(x => new FilesPersistenceClient(x.Resolve<IRestClient>("httpRepositoryClient"))));
        }
    }
}