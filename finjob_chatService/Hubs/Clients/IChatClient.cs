using finjob_chatService.Models;

namespace finjob_chatService.Hubs.Clients
{
    public interface IChatClient
    {
        Task ReceiveMessage(ChatMessage message);
    }
}
