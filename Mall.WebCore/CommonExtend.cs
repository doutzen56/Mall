using Mall.Common.Ioc.IocOptions;
using Mall.Core.Consts;
using Mall.Core.Redis;
using Mall.Core.Repositories;
using Mall.Core.Repositories.Interface;
using Mall.Interface.Jwt;
using Mall.Model.DTO;
using Mall.Service.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        /// <summary>
        /// 初始化应用程序基本的组件，如Redis、JWT、数据仓储等
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public static IServiceCollection Bootstrap(this IServiceCollection services, IConfiguration cfg)
        {
            services.AddTransient(typeof(IReadRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            //redis客户端注入
            services.AddSingleton<RedisProvider, RedisProvider>();
            //jwt工具类注入
            services.AddSingleton<ICustomJwtService, CustomHSJwtService>();
            services.Configure<JwtTokenOptions>(cfg.GetSection("JwtTokenOptions"));
            services.Configure<RedisConnOptions>(cfg.GetSection("RedisConnOptions"));
            return services;
        }
    }
}
