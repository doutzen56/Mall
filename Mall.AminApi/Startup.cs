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
            #region ȫ������������
            services.AddControllers(o =>
            {
                o.Filters.Add<ExceptionFilter>();
                o.Filters.Add<RequestLogFilter>();
            }).AddNewtonsoftJson();
            #endregion

            #region jwtУ��  
            JwtTokenOptions tokenOptions = new JwtTokenOptions();
            Configuration.Bind("JwtTokenOptions", tokenOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//�Ƿ���֤Issuer
                    ValidateAudience = true,//�Ƿ���֤Audience
                    ValidateLifetime = false,//�Ƿ���֤ʧЧʱ��
                    ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                    ValidAudience = tokenOptions.Audience,//
                    ValidIssuer = tokenOptions.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
                };
            });
            #endregion

            #region MyRegion

            #endregion
            //ע��SignalRʵʱͨѶ��Ĭ����json����
            services.AddSignalR(options =>
            {
                //�ͻ��˷������������󵽷����������Ĭ��30�룬�ĳ�4����
                options.ClientTimeoutInterval = TimeSpan.FromMinutes(4);
                //����˷������������󵽿ͻ��˼����Ĭ��15�룬�ĳ�2����
                options.KeepAliveInterval = TimeSpan.FromMinutes(2);
            });

            #region ����֧��
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
            #region Iocע��
            //Repositoryע��
            services.AddTransient(typeof(IReadRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            //redis�ͻ���ע��
            services.AddSingleton<RedisProvider, RedisProvider>();
            //jwt������ע��
            services.AddSingleton<ICustomJwtService, CustomHSJwtService>();
            services.Configure<JwtTokenOptions>(this.Configuration.GetSection("JwtTokenOptions"));
            services.Configure<RedisConnOptions>(this.Configuration.GetSection("RedisConnOptions"));
            //��ͨservice��ע��
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

            //���WebSocket֧��,����ʹ��WebSocket����
            app.UseWebSockets();

            //֧�ֿ���
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
