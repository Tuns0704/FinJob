using finjob_backend.Models;

namespace finjob_backend.Repository.IRepository
{
    public interface IConversationRepository : IRepository<Conversation>
    {
        Task<Conversation> GetByMembersAsync(string senderId, string receiverId);
        Task<List<Conversation>> GetByUserIdAsync(string userId);
        Task<Conversation> UpdateAsync(Conversation entity);
    }
}
