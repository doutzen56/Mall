using Mall.Common.Models;
using Mall.Interface.Service;
using Mall.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mall.UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private IGoodsService goodsService;
        public GoodsController(IGoodsService goodsService)
        {
            this.goodsService = goodsService;
        }

        [Route("add")]
        [HttpPost]
        public ApiResult Add([FromBody] Goods goods)
        {
            if (goodsService.Add(goods))
                return ApiResult.Ok();
            else
                return ApiResult.Fail();
        }
    }
}
