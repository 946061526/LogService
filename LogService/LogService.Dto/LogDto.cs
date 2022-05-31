using System;
using System.Collections.Generic;

namespace LogService.Dto
{
    public class LogDto
    {
    }

    /// <summary>
    /// 新增日志参数
    /// </summary>
    public class AddLogDto
    {
        /// <summary>
        /// 操作用户
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 等级（Trace, Debug, Info, Warn, Error）
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 异常内容
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
        /// 请求IP
        /// </summary>
        public string Ip { get; set; }
    }

    /// <summary>
    /// 查询日志返回
    /// </summary>
    public class LogResponse : AddLogDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime LogTime { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }
    }

    /// <summary>
    /// 日志分页返回
    /// </summary>
    public class LogPagedResponse
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 日志集合
        /// </summary>
        public List<LogResponse> DataList { get; set; }
    }

    /// <summary>
    /// 保存日志参数
    /// </summary>
    public class SaveLogDto : LogResponse
    {

    }
}
