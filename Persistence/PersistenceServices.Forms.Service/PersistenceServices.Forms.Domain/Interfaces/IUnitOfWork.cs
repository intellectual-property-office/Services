using System;
using System.Threading.Tasks;

namespace PersistenceServices.Forms.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : IEntity;

        Task<int> SaveChanges();
    }
}