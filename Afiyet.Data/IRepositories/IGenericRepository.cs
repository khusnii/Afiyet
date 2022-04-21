using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Afiyet.Data.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);

        T Update(T entity);

        IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null);

        Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);

        Task<T> GetAsync(Expression<Func<T, bool>> expression);


    }
}
