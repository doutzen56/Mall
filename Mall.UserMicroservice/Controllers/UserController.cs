using Mall.Common.Models;
using Mall.Core.Consts;
using Mall.Model.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Mall.UserMicroservice.Controllers
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
        [Route("verify")]
        [HttpGet]
        public RespResult CurrentUser()
        {
            IEnumerable<Claim> claimlist = HttpContext.AuthenticateAsync().Result.Principal.Claims;
            if (claimlist != null && claimlist.Count() > 0)
            {
                string userName = claimlist.FirstOrDefault(u => u.Type == AuthConst.AUTH_NAME).Value;
                string id = claimlist.FirstOrDefault(u => u.Type == AuthConst.AUTH_ID).Value;
                return RespResult.Ok(new UserInfo { Id = id, UserName = userName });
            }
            else
            {
                return RespResult.Fail("Token无效，请重新登陆");
            }
        }
    }
}
