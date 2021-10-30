using Mall.AminApi.Hubs;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace Mall.AminApi
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

            #region MyRegion

            #endregion
            //注入SignalR实时通讯，默认用json传输
            services.AddSignalR(options =>
            {
                //客户端发保持连接请求到服务端最长间隔，默认30秒，改成4分钟
                options.ClientTimeoutInterval = TimeSpan.FromMinutes(4);
                //服务端发保持连接请求到客户端间隔，默认15秒，改成2分钟
                options.KeepAliveInterval = TimeSpan.FromMinutes(2);
            });

            #region 跨域支持
            services.AddCors(option =>
            {
                option.AddPolicy("cors",
                    policy =>
                    {
                        policy.AllowAnyHeader()
                             .AllowAnyMethod()
                             .AllowCredentials()
                             .WithOrigins("http://localhost:53036");
                    });
            });
            #endregion
            services.AddHttpContextAccessor();
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

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mall.AminApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mall.AminApi v1"));
            }

            app.UseRouting();

            #region jwt 
            app.UseAuthentication();
            #endregion

            //添加WebSocket支持,优先使用WebSocket传输
            app.UseWebSockets();

            //支持跨域
            app.UseCors("cors");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
