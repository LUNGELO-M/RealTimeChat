using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RealTimeChat.Shared.Models;
using Microsoft.AspNetCore.Authorization;

namespace RealTimeChat.Server.Hubs
{
    [Authorize]
    public class MessagesHub : Hub
    {
        public async Task SendMesaage(Message message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            //await Clients.Client(connectionId).SendAsync();
        }
    }
}
