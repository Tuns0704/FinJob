using finjob_backend.Data;
using finjob_backend.Models;
using finjob_backend.Repository.IRepository;

namespace finjob_backend.Repository
{
    public class TopSkillRepository : Repository<TopSkill>, ITopSkillRepository
    {

        private readonly ApplicationDbContext _db;

        public TopSkillRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<TopSkill> UpdateAsync(TopSkill entity)
        {
            _db.TopSkills.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
