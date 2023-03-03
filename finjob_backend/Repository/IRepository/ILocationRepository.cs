using finjob_backend.Models;

namespace finjob_backend.Repository.IRepository
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<Location> UpdateAsync(Location entity);
    }
}
