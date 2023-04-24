using finjob_backend.Data;
using finjob_backend.Models;
using finjob_backend.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace finjob_backend.Repository
{
    public class UserApplyJobRepository : Repository<UserApplyJob>, IUserApplyJobRepository
    {
        private readonly ApplicationDbContext _db;

        public UserApplyJobRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<UserApplyJob> GetByJobAndUser(int jobId, string userId)
        {
            return await _db.UserApplyJobs
                .Where(j => j.JobId == jobId && j.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<UserApplyJob> UpdateAsync(UserApplyJob entity)
        {
            _db.UserApplyJobs.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
