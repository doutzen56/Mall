using Mall.Common.Models;
using Mall.Core.Filter;
using Mall.Interface.Service;
using Mall.Model.DTO;
using Mall.WebCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mall.OpenApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]

    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [Route("CreateOrder")]
        [HttpPost]
        [TypeFilter(typeof(Action2CommitFilter))]//避免重复提交
        public RespResult AddCart([FromForm] OrderDto orderDto)
        {
            UserInfo user = base.HttpContext.GetCurrentUserInfo();
            var orderId = this.orderService.CreateOrder(orderDto, user);

            return RespResult.Ok("下单成功", orderId);
        }
    }
}
