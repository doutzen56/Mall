using Mall.WebCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Mall.AminApi.Hubs
{
    public class ChatHub : Hub
    {
        //private readonly IHttpContextAccessor httpContextAccessor;
        //public ChatHub(IHttpContextAccessor httpContextAccessor)
        //{
        //    this.httpContextAccessor = httpContextAccessor;
        //}
        public override async Task OnConnectedAsync()
        {
            //var user = httpContextAccessor.HttpContext?.GetCurrentUserInfo();
            ////将同一个人的连接ID绑定到同一个分组，推送时就推送给这个分组
            //await Groups.AddToGroupAsync(Context.ConnectionId, user.UserName);

            //暂时先用all
            await Groups.AddToGroupAsync(Context.ConnectionId, "all");
        }
    }
}
