using Afiyet.Data.Contexts;
using Afiyet.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Afiyet.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal AfiyetDbContext context;
        internal DbSet<T> dbSet;

        public GenericRepository(AfiyetDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public async Task<T> CreateAsync(T entity)
        {
            var entry = await dbSet.AddAsync(entity);

            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await dbSet.FirstOrDefaultAsync(expression);

            if (entity is null)
            {
                return false;
            }

            dbSet.Remove(entity);

            return true;

        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null)
        {
            return expression == null ? dbSet : dbSet.Where(expression);
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return dbSet.FirstOrDefaultAsync(expression);
        }

        public T Update(T entity)
        {
            var entry = dbSet.Update(entity);

            return entry.Entity;
        }
    }
}
