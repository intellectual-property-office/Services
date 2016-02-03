using System;
using System.Threading.Tasks;

namespace PersistenceServices.Files.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : IEntity;

        Task<int> SaveChanges();
    }
}