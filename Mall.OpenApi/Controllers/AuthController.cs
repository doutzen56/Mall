using Mall.Common.Models;
using Mall.Interface.Jwt;
using Mall.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mall.OpenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly ICustomJwtService jwtService = null;
        private readonly IMemberService memberService;
        public AuthController(ILogger<AuthController> logger,
            ICustomJwtService jwtService,
            IMemberService memberService)
        {
            this.logger = logger;
            this.jwtService = jwtService;
            this.memberService = memberService;
        }
        [Route("Login")]
        [HttpPost]
        public RespResult Login([FromForm] string userName, [FromForm] string password)
        {
            var user = memberService.QueryUser(userName, password);
            if (user == null)
            {
                return RespResult.Fail("用户名或密码不正确。");
            }
            //如果查询到，生成token
            return RespResult.Ok("登录成功！",jwtService.GetToken(user));
        }
    }
}
