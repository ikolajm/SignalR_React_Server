using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;
// using ChatService;
using System.Linq;
using System;
using backend.Models;

namespace backend.Hubs
{
    public class ChatHub : Hub
    {
        // private readonly string _botUser;
        private readonly IDictionary<string, UserConnection> _connections;

        public ChatHub(IDictionary<string, UserConnection> connections)
        {
            // _botUser = "Official Chat Bot";            
            _connections = connections;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                _connections.Remove(Context.ConnectionId);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task JoinRoom(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.RoomIdString);

            _connections[Context.ConnectionId] = userConnection;

            // await Clients.Group(userConnection.RoomIdString).SendAsync("Notification", _botUser, $"{userConnection.User} has joined the room!");
        }

        public async Task SendMessage(User user, string message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                
                await Clients.Group(userConnection.RoomIdString).SendAsync("ReceiveMessage", user, message);
            }
        }
    }
}
