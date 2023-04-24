using finjob_backend.Data;
using finjob_backend.Models;
using finjob_backend.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace finjob_backend.Repository
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {

        private readonly ApplicationDbContext _db;

        public MessageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Message>> GetByConversationIdAsync(int conversationId)
        {
            return await _db.Messages
                .Where(m => m.ConversationId == conversationId)
                .ToListAsync();
        }

        public async Task<Message> UpdateAsync(Message entity)
        {
            _db.Messages.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
