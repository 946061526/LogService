using LogService.Core.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogService.Core.Database
{
    /// <summary>
    /// Mysql数据库
    /// </summary>
    public class MysqlDbContext : IBaseDbContext
    {
        private readonly IConfiguration _configuration;

        public MysqlDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql($"{_configuration["LogConfig:MySqlConn"]}");
        }

        /// <summary>
        /// 创建数据库实体映射
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogInfoEntityTypeConfigurationMysql());
        }
    }
}
