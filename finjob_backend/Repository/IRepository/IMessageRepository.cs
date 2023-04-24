using finjob_backend.Models;

namespace finjob_backend.Repository.IRepository
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<List<Message>> GetByConversationIdAsync(int conversationId);
        Task<Message> UpdateAsync(Message entity);
    }
}
