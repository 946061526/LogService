using LogService.Core.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogService.Core.Database
{
    /// <summary>
    /// Sqlite数据库
    /// </summary>
    public class SqliteDbContext : IBaseDbContext
    {
        private readonly IConfiguration _configuration;

        public SqliteDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Filename={_configuration["LogConfig:SqliteConn"]}");
        }

        /// <summary>
        /// 创建数据库实体映射
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogInfoEntityTypeConfigurationSqlite());
        }
    }
}
