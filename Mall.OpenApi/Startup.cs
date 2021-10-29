using Mall.Common.Ioc;
using Mall.Common.Ioc.IocOptions;
using Mall.Core.Filter;
using Mall.Core.Redis;
using Mall.Core.Repositories;
using Mall.Core.Repositories.Interface;
using Mall.Interface.Jwt;
using Mall.Service.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.OpenApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region 全局拦截器配置
            services.AddControllers(o =>
            {
                o.Filters.Add<ExceptionFilter>();
                o.Filters.Add<RequestLogFilter>();
            }).AddNewtonsoftJson();
            #endregion

            #region jwt校验  
            JwtTokenOptions tokenOptions = new JwtTokenOptions();
            Configuration.Bind("JwtTokenOptions", tokenOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = false,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = tokenOptions.Audience,//
                    ValidIssuer = tokenOptions.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
                };
            });
            #endregion

            #region 配置文件注入

            #endregion

            #region Ioc注入
            //Repository注入
            services.AddTransient(typeof(IReadRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            //redis客户端注入
            services.AddSingleton<RedisProvider, RedisProvider>();
            //jwt工具类注入
            services.AddSingleton<ICustomJwtService, CustomHSJwtService>();
            services.Configure<JwtTokenOptions>(this.Configuration.GetSection("JwtTokenOptions"));
            services.Configure<RedisConnOptions>(this.Configuration.GetSection("RedisConnOptions"));
            //普通service类注入
            services.DefaultServiceRegister();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            #region jwt 
            app.UseAuthentication();
            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
