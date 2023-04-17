using finjob_chatService.Hubs.Clients;
using Microsoft.AspNetCore.SignalR;

namespace finjob_chatService.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {

    }
}
