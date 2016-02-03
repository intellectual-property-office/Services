using System.Data.Entity;
using System.Web.Http;
using IPO.PersistenceServices.SerializationService;
using IPO.PersistenceServices.SerializationService.Interfaces;
using Microsoft.Practices.Unity;
using PersistenceServices.Forms.Domain.Entities;
using PersistenceServices.Forms.Domain.EntityFramework;
using PersistenceServices.Forms.Domain.Interfaces;
using PersistenceServices.Forms.Domain.Services;
using Unity.WebApi;

namespace PersistenceServices.Forms.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<ILoggerService, EmptyLoggerService>();
            container.Resolve<ILoggerService>().Info("Web app started.");

            container.RegisterType<DbContext, EntityFrameworkContext>();
            container.RegisterType<IRepository<FormDataEntity>, EntityFrameworkRepository<FormDataEntity>>();
            container.RegisterType<IUnitOfWork, EntityFrameworkUnitOfWork>();
            
            container.RegisterType<IFormsPersistenceService, FormsPersistenceService>();
            container.RegisterType<IFormsPersistenceFragmentService, FormsPersistenceFragmentService>();
            container.RegisterType<ISerializationService, JsonSerializationService>();
            container.RegisterType<IDataFragmentService, DataFragmentJsonService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}