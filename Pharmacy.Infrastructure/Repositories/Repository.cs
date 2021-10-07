using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Repositories;
using Pharmacy.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly PharmacyDbContext _dbContext;
        DbSet<T> _dbSet;

        public Repository(PharmacyDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual Task Add(T entity)
        {
            _dbSet.AddAsync(entity);
            return _dbContext.SaveChangesAsync();

        }
        public virtual Task Update(T entity)
        {
            _dbSet.Update(entity);
            return _dbContext.SaveChangesAsync();
        }

        public virtual Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            return _dbContext.SaveChangesAsync();
        }

        public Task<List<T>> Get(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter).ToListAsync();
        }

        public virtual Task<List<T>> GetAll()
        {
            return _dbSet.ToListAsync();
        }

    }
}
