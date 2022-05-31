using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LogService.Core.Database;
using LogService.Core.Repository;
using LogService.Core.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LogService.Test
{
    public class TestBase
    {
        protected readonly IServiceProvider serviceProvider;
        public TestBase()
        {
            IServiceCollection services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();

            services.AddSingleton<IConfiguration>(configuration);

            services.AddDbContext<IBaseDbContext, PostgreDbContext>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogService, Core.Service.LogService>();

            serviceProvider = services.BuildServiceProvider();

            NLog.LogManager.LoadConfiguration("NLog.config").GetCurrentClassLogger();
            NLog.LogManager.Configuration.Variables["MySqlConn"] = configuration["LogConfig:MySqlConn"];
            NLog.LogManager.Configuration.Variables["PostgreSqlConn"] = configuration["LogConfig:PostgreSqlConn"];
            NLog.LogManager.Configuration.Variables["SqliteConn"] = configuration["LogConfig:SqliteConn"];
        }

    }
}
