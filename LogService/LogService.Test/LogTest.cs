using LogService.Core.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace LogService.Test
{
    /// <summary>
    /// ��־�������
    /// </summary>
    public class LogTest : TestBase
    {
        ILogService _logService;

        /// <summary>
        /// ���
        /// </summary>
        [Fact]
        public void Add()
        {
            _logService = serviceProvider.GetService<ILogService>();

            _logService.Trace("username", "trace��־����", "�쳣����", "��������", "ϵͳ��־", "127.0.0.1");
            _logService.Debug("username", "debug��־����", "�쳣����", "��������", "ϵͳ��־", "127.0.0.1");
            _logService.Info("username", "info��־����", "�쳣����", "��������", "ϵͳ��־", "127.0.0.1");
            _logService.Warn("username", "warn��־����", "�쳣����", "��������", "ϵͳ��־", "127.0.0.1");
            _logService.Error("username", "error��־����", "�쳣����", "��������", "ϵͳ��־", "127.0.0.1");

            Assert.True(true);
        }

        /// <summary>
        /// ��ѯ
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
