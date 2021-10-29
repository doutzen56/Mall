using Mall.Model.DTO;
using Mall.WebCore.Consts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mall.WebCore
{
    public static class CommonExtend
    {
        /// <summary>
        /// 基于HttpContext,当前鉴权方式解析，获取用户信息
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static UserInfo GetCurrentUserInfo(this HttpContext httpContext)
        {
            IEnumerable<Claim> claimlist = httpContext.AuthenticateAsync().Result.Principal.Claims;
            return new UserInfo()
            {
                Id = claimlist.FirstOrDefault(u => u.Type == AuthConst.AUTH_ID).Value,
                UserName = claimlist.FirstOrDefault(u => u.Type == AuthConst.AUTH_NAME).Value ?? "匿名"
            };
        }
    }
}
