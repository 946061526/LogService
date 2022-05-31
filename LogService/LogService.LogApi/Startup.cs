using LogService.Core.Database;
using LogService.Core.Repository;
using LogService.Core.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text;

namespace LogService.LogApi
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
            var dbType = Configuration["LogConfig:DbType"];
            if (dbType == "PostgreSql")
            {
                services.AddDbContext<IBaseDbContext, PostgreDbContext>();
            }
            else if (dbType == "Sqlite")
            {
                services.AddDbContext<IBaseDbContext, SqliteDbContext>();
            }
            else if (dbType == "MySql")
            {
                services.AddDbContext<IBaseDbContext, MysqlDbContext>();
            }

            // 跨域配置
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "any",
                    builder => builder.SetIsOriginAllowed(_ => true).AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            #region Swagger配置

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "公共日志API",
                    Contact = new OpenApiContact
                    {
                        Name = "公共日志API",
                        Email = string.Empty,
                    },
                });
                c.CustomSchemaIds(type => type.FullName); // 解决相同类名会报错的问题

                // 为Swagger设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location); // 获取应用程序所在目录
                c.IncludeXmlComments(Path.Combine(basePath, "LogApiSwaggerDoc.xml"), true);
                c.IncludeXmlComments(Path.Combine(basePath, "ApiDto.xml"), true);
            });
            #endregion

            services.AddControllers();

            //相关服务注入
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogService, Core.Service.LogService>();

            services.AddMvc();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            NLog.LogManager.LoadConfiguration(Path.Combine(AppContext.BaseDirectory, "NLog.config")).GetCurrentClassLogger();
            NLog.LogManager.Configuration.Variables["PostgreSqlConn"] = Configuration["LogConfig:PostgreSqlConn"];
            NLog.LogManager.Configuration.Variables["MySqlConn"] = Configuration["LogConfig:MySqlConn"];
            NLog.LogManager.Configuration.Variables["SqliteConn"] = Configuration["LogConfig:SqliteConn"];
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);  //避免日志中的中文输出乱码

            // 添加Swagger有关中间件
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "公共日志API v1.0");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            // 使用跨域配置。CORS 中间件必须配置为在对 UseRouting 和 UseEndpoints的调用之间执行。 配置不正确将导致中间件停止正常运行。
            app.UseCors("any");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
