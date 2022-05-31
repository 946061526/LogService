using LogService.Core.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogService.Core.Database
{
    /// <summary>
    /// Postgre数据库
    /// </summary>
    public class PostgreDbContext : IBaseDbContext
    {
        private readonly IConfiguration _configuration;

        public PostgreDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(_configuration["LogConfig:PostgreSqlConn"]);
        }

        /// <summary>
        /// 创建数据库实体映射
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogEntityTypeConfigurationPostgre());
        }
    }
}
