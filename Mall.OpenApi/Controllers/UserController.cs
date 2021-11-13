using Mall.Common.Models;
using Mall.Core.Consts;
using Mall.Model.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;

namespace Mall.OpenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        public UserController(ILogger<UserController> logger)
        {
            this.logger = logger;
        }

        [Route("verify"), HttpGet]
        public RespResult CurrentUser()
        {
            var resultPrincipal = HttpContext.AuthenticateAsync().Result.Principal;
            if (resultPrincipal != null)
            {
                var claimList = resultPrincipal.Claims as Claim[] ?? resultPrincipal.Claims.ToArray();
                if (claimList.Any())
                {
                    var userName = claimList.FirstOrDefault(u => u.Type == AuthConst.AUTH_NAME)?.Value;
                    var id = claimList.FirstOrDefault(u => u.Type == AuthConst.AUTH_ID)?.Value;
                    return RespResult.Ok(new UserInfo { Id = id, UserName = userName });
                }
                else
                {
                    return RespResult.Fail("Token无效，请重新登陆");
                }
            }
            else
            {
                return RespResult.Fail("登录已过期，请重新登录");
            }
        }
    }
}
