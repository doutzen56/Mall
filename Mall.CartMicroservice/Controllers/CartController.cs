using Mall.Common.Models;
using Mall.Interface.Service;
using Mall.Model.DTO;
using Mall.WebCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Mall.CartMicroservice.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartService cartService;
        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }
        [Route("addCart")]
        [HttpPost]
        public RespResult AddCart([FromForm] CartDto cart)
        {
            UserInfo user = HttpContext.GetCurrentUserInfo();
            cartService.AddCart(cart, user);
            return RespResult.Ok("添加成功");
        }
        [Route("cart/{id}")]
        [HttpDelete]
        public RespResult DeleteCart(List<int> ids)
        {
            UserInfo user = HttpContext.GetCurrentUserInfo();
            cartService.DeleteCarts(ids, user.Id);
            return RespResult.Ok("删除成功");
        }
    }
}
