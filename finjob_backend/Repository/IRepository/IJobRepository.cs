using finjob_backend.Models;
using System.Linq.Expressions;

namespace finjob_backend.Repository.IRepository
{
    public interface IJobRepository : IRepository<Job>
    {
        Task<Job> UpdateAsync(Job entity);
    }
}
