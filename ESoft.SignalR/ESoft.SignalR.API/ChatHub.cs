using Microsoft.AspNetCore.SignalR;

namespace ESoft.SignalR.API
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // Broadcasts the message to all connected clients
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        //public async Task AddToGroup(string sessionId)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
        //    await Clients.Group(sessionId).SendAsync("Send", $"{Context.ConnectionId} has joined the group {sessionId}.");
        //}

        //// Send message to a specific session
        //public async Task SendMessageToSession(string sessionId, string user, string message)
        //{
        //    await Clients.Group(sessionId).SendAsync("ReceiveMessage", user, message);
        //}

        // Optional: Clean up if needed
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Optional: remove from groups if you want
            await base.OnDisconnectedAsync(exception);
        }
    }
}
    