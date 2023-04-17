using finjob_chatService.Hubs;
using finjob_chatService.Hubs.Clients;
using finjob_chatService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace finjob_chatService.Controllers
{
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub, IChatClient> _chatHub;

        public ChatController(IHubContext<ChatHub, IChatClient> chatHub)
        {
            _chatHub = chatHub;
        }

        [HttpPost("messages")]
        public async Task Post(ChatMessage message)
        {
            await _chatHub.Clients.All.ReceiveMessage(message);
        }
    }
}
