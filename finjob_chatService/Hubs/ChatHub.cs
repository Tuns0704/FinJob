using Microsoft.AspNetCore.SignalR;

namespace finjob_chatService.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            HttpContext httpContext = Context.GetHttpContext();

            string receiver = httpContext.Request.Query["userid"];
            string sender = Context.User.Claims.FirstOrDefault().Value;

            Groups.AddToGroupAsync(Context.ConnectionId, sender);
            if (!string.IsNullOrEmpty(receiver))
            {
                Groups.AddToGroupAsync(Context.ConnectionId, receiver);
            }

            return base.OnConnectedAsync();
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public Task SendMessageToUser(string receiver, string message)
        {
            return Clients.User(receiver).SendAsync("ReceiveMessage"
                , Context.User.Identity.Name, message);
        }
    }
}
