using System.Linq.Expressions;

namespace finjob_backend.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        /*        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);*/
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[]? includes);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, params Expression<Func<T, object>>[]? includes);

        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}
