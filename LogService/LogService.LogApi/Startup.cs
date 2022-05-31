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

            // ��������
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "any",
                    builder => builder.SetIsOriginAllowed(_ => true).AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            #region Swagger����

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "������־API",
                    Contact = new OpenApiContact
                    {
                        Name = "������־API",
                        Email = string.Empty,
                    },
                });
                c.CustomSchemaIds(type => type.FullName); // �����ͬ�����ᱨ�������

                // ΪSwagger����xml�ĵ�ע��·��
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location); // ��ȡӦ�ó�������Ŀ¼
                c.IncludeXmlComments(Path.Combine(basePath, "LogApiSwaggerDoc.xml"), true);
                c.IncludeXmlComments(Path.Combine(basePath, "ApiDto.xml"), true);
            });
            #endregion

            services.AddControllers();

            //��ط���ע��
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
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);  //������־�е������������

            // ���Swagger�й��м��
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "������־API v1.0");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            // ʹ�ÿ������á�CORS �м����������Ϊ�ڶ� UseRouting �� UseEndpoints�ĵ���֮��ִ�С� ���ò���ȷ�������м��ֹͣ�������С�
            app.UseCors("any");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
