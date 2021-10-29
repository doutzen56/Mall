using Mall.Common.Models;
using Mall.Interface.Service;
using Mall.Model.DTO;
using Mall.WebCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mall.OpenApi.Controllers
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
        public RespResult AddCart([FromForm] Cart cart)
        {
            UserInfo user = base.HttpContext.GetCurrentUserInfo();
            cartService.AddCart(cart, user);
            return RespResult.Ok();
        }
        //[Route("/cart/{id}")]
        //[HttpDelete]
        //public JsonResult DeleteCart(List<int> ids)
        //{
        //    UserInfo user = base.HttpContext.GetCurrentUserInfo();
        //    cartService.DeleteCarts(ids, user.Id);
        //    return new JsonResult(new AjaxResult()
        //    {
        //        Result = true,
        //        Message = "删除成功"
        //    });
        //}
    }
}
