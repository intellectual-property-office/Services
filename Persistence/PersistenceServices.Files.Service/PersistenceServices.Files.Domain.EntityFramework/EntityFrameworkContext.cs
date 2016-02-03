using System.Data.Entity;
using PersistenceServices.Files.Domain.Entities;
using PersistenceServices.Files.Domain.EntityFramework.Maps;

namespace PersistenceServices.Files.Domain.EntityFramework
{
    public class EntityFrameworkContext : DbContext
    {
        public IDbSet<FileBlob> FileBlobs { get; set; }

        public EntityFrameworkContext() : base("IPOFilesPersistence")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FileBlobMap());
            
            base.OnModelCreating(modelBuilder);
            Configuration.LazyLoadingEnabled = false;
        }
    }
}