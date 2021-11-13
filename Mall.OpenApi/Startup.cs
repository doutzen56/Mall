using Mall.Common.Ioc;
using Mall.Core.Filter;
using Mall.WebCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            #region ȫ������������
            services.AddControllers(o =>
            {
                o.Filters.Add<ExceptionFilter>();
                o.Filters.Add<RequestLogFilter>();
            }).AddNewtonsoftJson();
            #endregion

            #region jwtУ��  
            //JwtTokenOptions tokenOptions = new JwtTokenOptions();
            //Configuration.Bind(OptionsConst.JWT_TOKEN_OPTIONS, tokenOptions);

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,//�Ƿ���֤Issuer
            //        ValidateAudience = true,//�Ƿ���֤Audience
            //        ValidateLifetime = false,//�Ƿ���֤ʧЧʱ��
            //        ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
            //        ValidAudience = tokenOptions.Audience,//
            //        ValidIssuer = tokenOptions.Issuer,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
            //    };
            //});
            #endregion

            #region �����ļ�ע��

            #endregion

            #region Iocע��
            ////Repositoryע��
            //services.AddTransient(typeof(IReadRepository<>), typeof(Repository<>));
            //services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            ////redis�ͻ���ע��
            //services.AddSingleton<RedisProvider, RedisProvider>();
            ////jwt������ע��
            //services.AddSingleton<ICustomJwtService, CustomHSJwtService>();
            //services.Configure<JwtTokenOptions>(this.Configuration.GetSection(OptionsConst.JWT_TOKEN_OPTIONS));
            //services.Configure<RedisConnOptions>(this.Configuration.GetSection(OptionsConst.REDIS_CONN_OPTIONS));
            services.Bootstrap(Configuration);

            //��ͨservice��ע��
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
