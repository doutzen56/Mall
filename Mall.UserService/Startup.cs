using Mall.Common.Ioc;
using Mall.Core.Filter;
using Mall.Core.Repositories;
using Mall.Core.Repositories.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mall.UserService
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
            #region ·þÎñ×¢²á

            services.AddTransient(typeof(IReadRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.DefaultRegister();
            #endregion

            #region À¹½ØÆ÷×¢²á
            services.AddControllers(o =>
            {
                o.Filters.Add<RequestLogFilter>();
                o.Filters.Add<ExceptionFilter>();
            });
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mall.UserService", Version = "v1" });
            });
            #endregion

            #region ¿çÓòÅäÖÃ
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyMethod()
                          .AllowAnyOrigin()
                          .AllowAnyHeader();
                });
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mall.UserService v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("default");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
