using finjob_backend.Data;
using finjob_backend.Models;
using finjob_backend.Repository.IRepository;

namespace finjob_backend.Repository
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LocationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Location> UpdateAsync(Location entity)
        {
            _db.Locations.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
