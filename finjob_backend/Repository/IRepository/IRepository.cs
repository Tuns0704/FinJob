using finjob_backend.Models;
using System.Linq.Expressions;

namespace finjob_backend.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<PaginationResult<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, int pageSize = 0, int pageNumber = 1, params Expression<Func<T, object>>[]? includes);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, params Expression<Func<T, object>>[]? includes);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}
