using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogService.CommonService
{
    /// <summary>
    /// 公共日志服务
    /// </summary>
    public class LogCommonService
    {
        private static Logger _logger = null;
        private static readonly object _loggerObj = new object();

        static LogCommonService()
        {
            var dbType = AppSettingsHelper.Configuration["LogConfig:DbType"];
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
        public static void Trace(string user_name, string message, string exception, string object_key, string module_type, string ip)
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
        public static void Debug(string user_name, string message, string exception, string object_key, string module_type, string ip)
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
        public static void Info(string user_name, string message, string exception, string object_key, string module_type, string ip)
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
        public static void Warn(string user_name, string message, string exception, string object_key, string module_type, string ip)
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
        public static void Error(string user_name, string message, string exception, string object_key, string module_type, string ip)
        {
            var logEvent = GetLog(LogLevel.Error, user_name, message, exception, object_key, module_type, ip);
            _logger.Error(logEvent);
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
        private static LogEventInfo GetLog(LogLevel level, string user_name, string message, string exception, string object_key, string module_type, string ip)
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
            logEventInfo.Properties["log_time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            logEventInfo.Properties["time_stamp"] = GetTimestamp();

            return logEventInfo;
        }

        /// <summary>
        /// 将当前时间转化为时间戳
        /// </summary>
        /// <returns>Unix时间戳格式</returns>
        private static long GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
    }
}
