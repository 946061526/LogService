using LogService.Core.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace LogService.Test
{
    /// <summary>
    /// 日志服务测试
    /// </summary>
    public class LogTest : TestBase
    {
        ILogService _logService;

        /// <summary>
        /// 添加
        /// </summary>
        [Fact]
        public void Add()
        {
            _logService = serviceProvider.GetService<ILogService>();

            _logService.Trace("username", "trace日志内容", "异常内容", "操作对象", "系统日志", "127.0.0.1");
            _logService.Debug("username", "debug日志内容", "异常内容", "操作对象", "系统日志", "127.0.0.1");
            _logService.Info("username", "info日志内容", "异常内容", "操作对象", "系统日志", "127.0.0.1");
            _logService.Warn("username", "warn日志内容", "异常内容", "操作对象", "系统日志", "127.0.0.1");
            _logService.Error("username", "error日志内容", "异常内容", "操作对象", "系统日志", "127.0.0.1");

            Assert.True(true);
        }

        /// <summary>
        /// 查询
        /// </summary>
        [Theory]
        [InlineData(null, null, "", "", "", "", 1, 20)]
        public async void GetList(DateTime? startTime, DateTime? endTime, string level, string objectKey, string message, string moduleType, int pageIndex, int pageSize = 20)
        {
            _logService = serviceProvider.GetService<ILogService>();

            var list = await _logService.GetListAsync(startTime, endTime, level, objectKey, message, moduleType, pageIndex, pageSize);

            Assert.True(true);
        }

    }
}
