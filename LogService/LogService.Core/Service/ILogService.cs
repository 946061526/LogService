using LogService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogService.Core.Service
{
    /// <summary>
    /// 日志服务
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Trace日志
        /// </summary>
        /// <param name="user_name">用户名</param>
        /// <param name="message">日志内容</param>
        /// <param name="exception">异常内容</param>
        /// <param name="object_key">操作对象</param>
        /// <param name="module_type">日志类型</param>
        /// <param name="ip">来源IP</param>
        void Trace(string user_name, string message, string exception, string object_key, string module_type, string ip);

        /// <summary>
        /// Debug日志
        /// </summary>
        /// <param name="user_name">用户名</param>
        /// <param name="message">日志内容</param>
        /// <param name="exception">异常内容</param>
        /// <param name="object_key">操作对象</param>
        /// <param name="module_type">日志类型</param>
        /// <param name="ip">来源IP</param>
        void Debug(string user_name, string message, string exception, string object_key, string module_type, string ip);

        /// <summary>
        /// Info日志
        /// </summary>
        /// <param name="user_name">用户名</param>
        /// <param name="message">日志内容</param>
        /// <param name="exception">异常内容</param>
        /// <param name="object_key">操作对象</param>
        /// <param name="module_type">日志类型</param>
        /// <param name="ip">来源IP</param>
        void Info(string user_name, string message, string exception, string object_key, string module_type, string ip);

        /// <summary>
        /// Warn日志
        /// </summary>
        /// <param name="user_name">用户名</param>
        /// <param name="message">日志内容</param>
        /// <param name="exception">异常内容</param>
        /// <param name="object_key">操作对象</param>
        /// <param name="module_type">日志类型</param>
        /// <param name="ip">来源IP</param>
        void Warn(string user_name, string message, string exception, string object_key, string module_type, string ip);

        /// <summary>
        /// Error日志
        /// </summary>
        /// <param name="user_name">用户名</param>
        /// <param name="message">日志内容</param>
        /// <param name="exception">异常内容</param>
        /// <param name="object_key">操作对象</param>
        /// <param name="module_type">日志类型</param>
        /// <param name="ip">来源IP</param>
        void Error(string user_name, string message, string exception, string object_key, string module_type, string ip);

        /// <summary>
        /// 新增日志
        /// </summary>
        /// <param name="level">等级</param>
        /// <param name="user_name">用户名</param>
        /// <param name="message">日志内容</param>
        /// <param name="exception">异常内容</param>
        /// <param name="object_key">操作对象</param>
        /// <param name="module_type">日志类型</param>
        /// <param name="ip">来源IP</param>
        void Add(string level, string user_name, string message, string exception, string object_key, string module_type, string ip);

        /// <summary>
        /// 新增日志
        /// </summary>
        /// <param name="level">等级</param>
        /// <param name="user_name">用户名</param>
        /// <param name="message">日志内容</param>
        /// <param name="exception">异常内容</param>
        /// <param name="object_key">操作对象</param>
        /// <param name="module_type">日志类型</param>
        /// <param name="ip">来源IP</param>
        Task<bool> AddAsync(string level, string user_name, string message, string exception, string object_key, string module_type, string ip);

        /// <summary>
        /// 分页查询日志列表
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="level">等级(Trace,Debug,Info,Warn,Error)</param>
        /// <param name="objectKey">操作对象</param>
        /// <param name="message">内容(模糊匹配)</param>
        /// <param name="moduleType">类型</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        Task<(int Total, List<LogResponse> List)> GetListAsync(DateTime? startTime, DateTime? endTime, string level, string objectKey, string message, string moduleType, int pageIndex, int pageSize = 20);
    }
}
