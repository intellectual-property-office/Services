using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PersistenceServices.Forms.Domain.Interfaces;

namespace PersistenceServices.Forms.Domain.EntityFramework
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EntityFrameworkRepository(DbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {
            return predicate != null
                ? await _dbSet.Where(predicate).ToListAsync()
                : await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual T Add(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public virtual T Remove(T entity)
        {
            return _dbContext.Entry(entity).State == EntityState.Detached
                ? _dbSet.Attach(entity)
                : _dbSet.Remove(entity);
        }

        public virtual T Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}