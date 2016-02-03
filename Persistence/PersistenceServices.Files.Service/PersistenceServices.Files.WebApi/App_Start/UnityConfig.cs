using System.Data.Entity;
using System.Web.Http;
using PersistenceServices.Files.Domain.Entities;
using PersistenceServices.Files.Domain.Interfaces;
using PersistenceServices.Files.Domain.Services;
using PersistenceServices.Files.Domain.EntityFramework;
using Microsoft.Practices.Unity;
using Unity.WebApi;

namespace PersistenceServices.Files.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<ILoggerService, EmptyLoggerService>();
            container.Resolve<ILoggerService>().Info("Web app started.");

            container.RegisterType<DbContext, EntityFrameworkContext>();
            container.RegisterType<IRepository<FileBlob>, EntityFrameworkRepository<FileBlob>>();
            container.RegisterType<IUnitOfWork, EntityFrameworkUnitOfWork>(new TransientLifetimeManager());
            
            container.RegisterType<IFilesPersistenceService, FilesPersistenceService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}