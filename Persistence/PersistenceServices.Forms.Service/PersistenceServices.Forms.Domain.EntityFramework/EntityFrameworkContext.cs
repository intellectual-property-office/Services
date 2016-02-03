using System.Data.Entity;
using PersistenceServices.Forms.Domain.EntityFramework.Maps;

namespace PersistenceServices.Forms.Domain.EntityFramework
{
    public class EntityFrameworkContext : DbContext
    {
        public EntityFrameworkContext() : base("IPOFormsPersistence")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersistenceEntityMap());

            base.OnModelCreating(modelBuilder);
            Configuration.LazyLoadingEnabled = false;
        }
    }
}