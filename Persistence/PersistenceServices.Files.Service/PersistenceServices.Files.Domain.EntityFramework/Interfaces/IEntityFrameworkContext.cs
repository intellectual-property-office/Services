using System.Data.Entity;
using PersistenceServices.Files.Domain.Entities;

namespace PersistenceServices.Files.Domain.EntityFramework.Interfaces
{
    public interface IEntityFrameworkContext
    {
        IDbSet<FileBlob> FileBlobs { get; set; }

        int SaveChanges();
    }
}