using finjob_backend.Data;
using finjob_backend.Models;
using finjob_backend.Repository.IRepository;

namespace finjob_backend.Repository
{
    public class PositionRepository : Repository<Position>, IPositionRepository
    {
        private readonly ApplicationDbContext _db;

        public PositionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Position> UpdateAsync(Position entity)
        {
            _db.Positions.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
