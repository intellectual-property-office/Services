using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PersistenceServices.Forms.Domain.Interfaces;

namespace PersistenceServices.Forms.Domain.Xml
{
    public class XmlRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly IList<T> _entityList;

        public XmlRepository(IList<T> entityList)
        {
            _entityList = entityList;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return await Task.Run(() => _entityList.Where(predicate.Compile()));
            }

            return await Task.Run(() => _entityList);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() => _entityList.FirstOrDefault(predicate.Compile()));
        }

        public T Add(T entity)
        {
            entity.Id = _entityList.Count == 0 ? 1 :_entityList.Max(e => e.Id) + 1;
            _entityList.Add(entity);
            return entity;
        }

        public T Remove(T entity)
        {
            _entityList.Remove(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _entityList.Remove(_entityList.FirstOrDefault(e => e.Id == entity.Id));
            _entityList.Add(entity);
            return entity;
        }
    }
}