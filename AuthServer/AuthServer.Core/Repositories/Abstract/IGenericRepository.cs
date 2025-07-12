using System.Linq.Expressions;

namespace AuthServer.Core.Repositories.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Delete(T entity);
        T Update(T entity);
    }
}
