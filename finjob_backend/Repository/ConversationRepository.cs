using finjob_backend.Data;
using finjob_backend.Models;
using finjob_backend.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace finjob_backend.Repository
{
    public class ConversationRepository : Repository<Conversation>, IConversationRepository
    {

        private readonly ApplicationDbContext _db;

        public ConversationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Conversation> GetByMembersAsync(string senderId, string receiverId)
        {
            return await _db.Conversations
                .FirstOrDefaultAsync(c =>
                    (c.SenderId == senderId && c.ReceiverId == receiverId) ||
                    (c.SenderId == receiverId && c.ReceiverId == senderId));
        }

        public async Task<List<Conversation>> GetByUserIdAsync(string userId)
        {
            return await _db.Conversations
                .Where(c => c.SenderId == userId || c.ReceiverId == userId)
                .ToListAsync();
        }

        public async Task<Conversation> UpdateAsync(Conversation entity)
        {
            _db.Conversations.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
