using Mall.AminApi.Hubs;
using Mall.Common.Models;
using Mall.Interface.Service;
using Mall.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;

namespace Mall.AminApi.Controllers
{
    /// <summary>
    /// admin后台Api
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly IHubContext<ChatHub> messageHub;
        private readonly IGoodsService goodsService;
        public GoodsController(IHubContext<ChatHub> messageHub, IGoodsService goodsService)
        {
            this.messageHub = messageHub;
            this.goodsService = goodsService;
        }
        [Route("update")]
        [HttpPost]
        [Authorize]
        public RespResult Update([FromForm] Goods goods)
        {
            if (goodsService.Update(goods))
            {
                //推送消息
                messageHub.Clients.Groups("all").SendAsync("newPrice", goods.Price);
            }
            return RespResult.Ok();
        }
        /// <summary>
        /// 模拟真实用户操作数据
        /// </summary>
        /// <returns></returns>
        [Route("test")]
        [HttpGet]
        public RespResult Test()
        {
            var a = new Random().Next(100, 200);
            if (goodsService.Update(new Goods { Id = 1, Price = a, Code = "Test", Color = 2, Name = "耳机", Stock = 999, Status = 1 }))
            {
                //推送消息
                messageHub.Clients.Groups("all").SendAsync("newPrice", a);
            }
            return RespResult.Ok();
        }
    }
}
