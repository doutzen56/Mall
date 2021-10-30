using Mall.Hubs.Interface;
using Mall.Hubs.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mall.Hubs.Hubs
{
    public class UpdatePriceHub : Hub<IPushMsg>
    {
        public override async Task OnConnectedAsync()
        {
            var user = Context.ConnectionId;

            await Clients.Client(user).GetMyId(new ClientMessageModel { UserId = user, Context = $"回来了{DateTime.Now:yyyy-MM:dd HH:mm:ss}" });
            await Clients.AllExcept(user).ReceiveMessage(new ClientMessageModel { UserId = user, Context = $"进来了{DateTime.Now:yyyy-MM:dd HH:mm:ss}" });
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = Context.ConnectionId;

            await Clients.All.ReceiveMessage(new ClientMessageModel { UserId = user, Context = $"{user}离开了{DateTime.Now:yyyy-MM:dd HH:mm:ss}" });
            await base.OnDisconnectedAsync(exception);
        }
    }
}
