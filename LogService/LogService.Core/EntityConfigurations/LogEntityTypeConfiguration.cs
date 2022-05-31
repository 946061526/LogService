using LogService.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogService.Core.EntityConfigurations
{
    /// <summary>
    /// 日志表映射类
    /// </summary>
    public class LogEntityTypeConfiguration : IEntityTypeConfiguration<LogEntity>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public void Configure(EntityTypeBuilder<LogEntity> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Table & Column Mappings
        }

        #endregion
    }

    /// <summary>
    /// 日志表映射类 Postgre
    /// </summary>
    public class LogEntityTypeConfigurationPostgre : IEntityTypeConfiguration<LogEntity>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public void Configure(EntityTypeBuilder<LogEntity> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);
            // Table & Column Mappings
            builder.ToTable("loginfo");
            builder.Property(t => t.Id).HasColumnName("id").HasMaxLength(50).IsRequired();
            builder.Property(t => t.UserName).HasColumnName("user_name").HasMaxLength(30);
            builder.Property(t => t.Message).HasColumnName("message");
            builder.Property(t => t.Exception).HasColumnName("exception");
            builder.Property(t => t.ObjectKey).HasColumnName("object_key").HasMaxLength(100);
            builder.Property(t => t.ModuleType).HasColumnName("module_type").HasMaxLength(100);
            builder.Property(t => t.Level).HasColumnName("level");
            builder.Property(t => t.Ip).HasColumnName("ip").HasMaxLength(50);
            builder.Property(t => t.LogTime).HasColumnName("log_time").HasColumnType("timestamp without time zone").HasDefaultValue(DateTime.Now);
            builder.Property(t => t.Timestamp).HasColumnName("time_stamp");
            builder.HasIndex(t => t.Timestamp).HasName("index_log_time");
            builder.HasIndex(t => t.Level).HasName("index_log_level");
        }

        #endregion
    }

    /// <summary>
    /// 日志表映射类 Sqlite
    /// </summary>
    public class LogInfoEntityTypeConfigurationSqlite : IEntityTypeConfiguration<LogEntity>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public void Configure(EntityTypeBuilder<LogEntity> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Table & Column Mappings
            builder.ToTable("loginfo");
            builder.Property(t => t.Id).HasColumnName("id").HasMaxLength(50).IsRequired();
            builder.Property(t => t.UserName).HasColumnName("user_name").HasMaxLength(30);
            builder.Property(t => t.Message).HasColumnName("message");
            builder.Property(t => t.Exception).HasColumnName("exception");
            builder.Property(t => t.ObjectKey).HasColumnName("object_key").HasMaxLength(100);
            builder.Property(t => t.ModuleType).HasColumnName("module_type").HasMaxLength(100);
            builder.Property(t => t.Level).HasColumnName("level");
            builder.Property(t => t.Ip).HasColumnName("ip").HasMaxLength(50);
            builder.Property(t => t.LogTime).HasColumnName("log_time").HasDefaultValue(DateTime.Now);
            builder.Property(t => t.Timestamp).HasColumnName("time_stamp");

            builder.HasIndex(t => t.Timestamp).HasName("log_ind_time");
            builder.HasIndex(t => t.Level).HasName("log_ind_level");
        }

        #endregion
    }

    /// <summary>
    /// 日志表映射类 Mysql
    /// </summary>
    public class LogInfoEntityTypeConfigurationMysql : IEntityTypeConfiguration<LogEntity>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public void Configure(EntityTypeBuilder<LogEntity> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Table & Column Mappings
            builder.ToTable("loginfo");
            builder.Property(t => t.Id).HasColumnName("id").HasMaxLength(50).IsRequired();
            builder.Property(t => t.UserName).HasColumnName("user_name").HasMaxLength(30);
            builder.Property(t => t.Message).HasColumnName("message");
            builder.Property(t => t.Exception).HasColumnName("exception");
            builder.Property(t => t.ObjectKey).HasColumnName("object_key").HasMaxLength(100);
            builder.Property(t => t.ModuleType).HasColumnName("module_type").HasMaxLength(100);
            builder.Property(t => t.Level).HasColumnName("level");
            builder.Property(t => t.Ip).HasColumnName("ip").HasMaxLength(50);
            builder.Property(t => t.LogTime).HasColumnName("log_time").HasDefaultValue(DateTime.Now);
            builder.Property(t => t.Timestamp).HasColumnName("time_stamp");

            builder.HasIndex(t => t.Timestamp).HasName("index_log_time");
            builder.HasIndex(t => t.Level).HasName("index_log_level");
        }

        #endregion
    }
}
