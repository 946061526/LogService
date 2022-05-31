using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LogService.Core.Domain
{
    /// <summary>
    /// 日志实体类
    /// </summary>
    public class LogEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 操作用户
        /// </summary>        
        public string UserName { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 日常内容
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// 操作对象
        /// </summary>
        public string ObjectKey { get; set; }

        /// <summary>
        /// 日志类型(模块名称或日志来源)
        /// </summary>
        public string ModuleType { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 请求IP
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime LogTime { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }
    }
}
