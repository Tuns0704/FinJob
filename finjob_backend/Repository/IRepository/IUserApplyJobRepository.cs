using finjob_backend.Models;

namespace finjob_backend.Repository.IRepository
{
    public interface IUserApplyJobRepository : IRepository<UserApplyJob>
    {
        Task<UserApplyJob> GetByJobAndUser(int jobId, string userId);
        Task<UserApplyJob> UpdateAsync(UserApplyJob entity);
    }
}
