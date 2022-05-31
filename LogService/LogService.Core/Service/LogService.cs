using LogService.Core.Repository;
using LogService.Dto;
using LogService.Tools;
using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogService.Core.Service
{
    /// <summary>
    /// 日志服务
    /// </summary>
    public class LogService : ILogService
    {
        /// <summary>
        /// 日志实例
        /// </summary>
        private static Logger _logger = null;

        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object _loggerObj = new object();

        /// <summary>
        /// 日志仓储
        /// </summary>
        private readonly ILogRepository _logRepository;

        public LogService(IConfiguration configuration, ILogRepository logRepository)
        {
            _logRepository = logRepository;

            var dbType = configuration["LogConfig:DbType"];
            if (_logger == null)
            {
                lock (_loggerObj)
                {
                    if (_logger == null)
                    {
                        switch (dbType)
                        {
                            case "PostgreSql":
                                _logger = LogManager.GetLogger("PostgreSql");//写PostgreSql
                                break;
                            case "MySql":
                                _logger = LogManager.GetLogger("MySql");//写MySql
                                break;
                            case "Sqlite":
                                _logger = LogManager.GetLogger("Sqlite");//写Sqlite
                                break;
                            case "File":
                                _logger = LogManager.GetLogger("File");//写文件
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Trace日志
        /// </summary>
        /// <param name="user_name">用户名</param>
        /// <param name="message">日志内容</param>
        /// <param name="exception">异常内容</param>
        /// <param name="object_key">操作对象</param>
        /// <param name="module_type">日志类型</param>
        /// <param name="ip">来源IP</param>
        public void Trace(string user_name, string message, string exception, string object_key, string module_type, string ip)
        {
            var logEvent = GetLog(LogLevel.Trace, user_name, message, exception, object_key, module_type, ip);
            _logger.Trace(logEvent);
        }

        /// <summary>
        /// Debug日志
        /// </summary>
        /// <param name="user_name">用户名</param>
        /// <param name="message">日志内容</param>
        /// <param name="exception">异常内容</param>
        /// <param name="object_key">操作对象</param>
        /// <param name="module_type">日志类型</param>
        /// <param name="ip">来源IP</param>
        public void Debug(string user_name, string message, string exception, string object_key, string module_type, string ip)
        {
            var logEvent = GetLog(LogLevel.Debug, user_name, message, exception, object_key, module_type, ip);
            _logger.Debug(logEvent);
        }

        /// <summary>
        /// Warn日志
        /// </summary>
        /// <param name="user_name">用户名</param>
        /// <param name="message">日志内容</param>
        /// <param name="exception">异常内容</param>
        /// <param name="object_key">操作对象</param>
        /// <param name="module_type">日志类型</param>
        /// <param name="ip">来源IP</param>
        public void Info(string user_name, string message, string exception, string object_key, string module_type, string ip)
        {
            var logEvent = GetLog(LogLevel.Info, user_name, message, exception, object_key, module_type, ip);
            _logger.Info(logEvent);
        }

        /// <summary>
        /// Warn日志
        /// </summary>
        /// <param name="user_name">用户名</param>
        /// <param name="message">日志内容</param>
        /// <param name="exception">异常内容</param>
        /// <param name="object_key">操作对象</param>
        /// <param name="module_type">日志类型</param>
        /// <param name="ip">来源IP</param>
        public void Warn(string user_name, string message, string exception, string object_key, string module_type, string ip)
        {
            var logEvent = GetLog(LogLevel.Warn, user_name, message, exception, object_key, module_type, ip);
            _logger.Warn(logEvent);
        }

        /// <summary>
        /// Error日志
        /// </summary>
        /// <param name="user_name">用户名</param>
        /// <param name="message">日志内容</param>
        /// <param name="exception">异常内容</param>
        /// <param name="object_key">操作对象</param>
        /// <param name="module_type">日志类型</param>
        /// <param name="ip">来源IP</param>
        public void Error(string user_name, string message, string exception, string object_key, string module_type, string ip)
        {
            var logEvent = GetLog(LogLevel.Error, user_name, message, exception, object_key, module_type, ip);
            _logger.Error(logEvent);
        }

        /// <summary>
        /// Error日志
        /// </summary>
        /// <param name="level">等级</param>
        /// <param name="user_name">用户名</param>
        /// <param name="message">日志内容</param>
        /// <param name="exception">异常内容</param>
        /// <param name="object_key">操作对象</param>
        /// <param name="module_type">日志类型</param>
        /// <param name="ip">来源IP</param>
        public void Add(string level, string user_name, string message, string exception, string object_key, string module_type, string ip)
        {
            LogEventInfo logEvent;
            switch (level.ToLower())
            {
                case "trace":
                    logEvent = GetLog(LogLevel.Trace, user_name, message, exception, object_key, module_type, ip);
                    _logger.Trace(logEvent);
                    break;
                case "debug":
                    logEvent = GetLog(LogLevel.Debug, user_name, message, exception, object_key, module_type, ip);
                    _logger.Debug(logEvent);
                    break;
                case "info":
                    logEvent = GetLog(LogLevel.Info, user_name, message, exception, object_key, module_type, ip);
                    _logger.Info(logEvent);
                    break;
                case "warn":
                    logEvent = GetLog(LogLevel.Warn, user_name, message, exception, object_key, module_type, ip);
                    _logger.Warn(logEvent);
                    break;
                case "error":
                    logEvent = GetLog(LogLevel.Error, user_name, message, exception, object_key, module_type, ip);
                    _logger.Error(logEvent);
                    break;
            }
        }

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
        public async Task<bool> AddAsync(string level, string user_name, string message, string exception, string object_key, string module_type, string ip)
        {
            var dto = new SaveLogDto
            {
                Id = Guid.NewGuid().ToString("n"),
                UserName = user_name,
                Message = message,
                Exception = exception,
                ObjectKey = object_key,
                ModuleType = module_type,
                Ip = ip,
                Level = level,
                LogTime = DateTime.Now,
                Timestamp = Common.GetTimestamp(),
            };

            return await _logRepository.AddAsync(dto);
        }

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
        public async Task<(int Total, List<LogResponse> List)> GetListAsync(DateTime? startTime, DateTime? endTime, string level, string objectKey, string message, string moduleType, int pageIndex, int pageSize = 20)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 20;
            }

            if (!startTime.HasValue)
            {
                startTime = DateTime.Now.Date.AddDays(-6);
            }

            if (!endTime.HasValue)
            {
                endTime = DateTime.Now.Date.AddDays(1);
            }

            if (endTime.Value == endTime.Value.Date)
            {
                endTime = endTime.Value.AddDays(1);
            }

            DateTime eTime = endTime.Value.AddSeconds(1);
            //long sTimeStamp = CommonUtils.GetTimestamp(startTime.Value);
            //long eTimeStamp = CommonUtils.GetTimestamp(eTime);
            var result = await _logRepository.GetListAsync(startTime, eTime, level, objectKey, message, moduleType, pageIndex, pageSize);
            return result;
        }

        /// <summary>
        /// 日志属性
        /// </summary>
        /// <param name="level"></param>
        /// <param name="user_name"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="object_key"></param>
        /// <param name="module_type"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        private LogEventInfo GetLog(LogLevel level, string user_name, string message, string exception, string object_key, string module_type, string ip)
        {
            LogEventInfo logEventInfo = new LogEventInfo(level, "", message);
            logEventInfo.Properties["id"] = Guid.NewGuid().ToString("N");
            logEventInfo.Properties["user_name"] = user_name;
            //logEventInfo.Properties["level"] = level.ToString().ToLower();
            //logEventInfo.Properties["message"] = message;
            logEventInfo.Properties["exception"] = exception;
            logEventInfo.Properties["object_key"] = object_key;
            logEventInfo.Properties["module_type"] = module_type;
            logEventInfo.Properties["ip"] = ip;
            //logEventInfo.Properties["log_time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            logEventInfo.Properties["time_stamp"] = Common.GetTimestamp();

            return logEventInfo;
        }
    }
}
