using finjob_backend.Data;
using finjob_backend.Models;
using finjob_backend.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace finjob_backend.Repository
{
    public class JobRepository : Repository<Job> , IJobRepository
    {
        private readonly ApplicationDbContext _db;

        public JobRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Job> UpdateAsync(Job entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Jobs.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
