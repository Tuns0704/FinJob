using finjob_backend.Models;

namespace finjob_backend.Repository.IRepository
{
    public interface IPositionRepository : IRepository<Position>
    {
        Task<Position> UpdateAsync(Position entity);
    }
}
