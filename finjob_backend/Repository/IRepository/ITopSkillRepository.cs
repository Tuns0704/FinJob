using finjob_backend.Models;

namespace finjob_backend.Repository.IRepository
{
    public interface ITopSkillRepository : IRepository<TopSkill>
    {
        Task<TopSkill> UpdateAsync(TopSkill entity);
    }
}
