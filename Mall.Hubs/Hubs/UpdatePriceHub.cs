using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mall.Hubs.Hubs
{
    public class UpdatePriceHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user + "[" + DateTime.Now.ToLongTimeString() + "]", message);
        }
        public async Task SendMessageToUser(string user, string message)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", user + ":" + DateTime.Now.ToLongTimeString() + "]", message);
        }
    }
}
