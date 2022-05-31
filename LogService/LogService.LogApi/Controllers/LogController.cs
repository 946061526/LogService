using LogService.Core.Service;
using LogService.Dto;
using LogService.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LogService.LogApi.Controllers
{
    /// <summary>
    /// 日志服务（支持Trace, Debug, Info, Warn, Error）
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        /// <summary>
        /// 日志等级
        /// </summary>
        private readonly string[] _logLevels = new string[] { "trace", "debug", "info", "warn", "error" };
        /// <summary>
        /// http上下文
        /// </summary>
        private IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 日志服务
        /// </summary>
        private readonly ILogService _logService;

        public LogController(ILogService logService, IHttpContextAccessor httpContextAccessor)
        {
            _logService = logService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="model">日志对象</param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<ApiResultModel<string>> AddAsync([FromBody] AddLogDto model)
        {
            #region 验参

            if (string.IsNullOrEmpty(model.Level))
            {
                return new ApiResultModel<string>() { message = "日志等级不能为空" };
            }
            else if (!_logLevels.Any(x => x == model.Level.ToLower()))
            {
                return new ApiResultModel<string>() { message = "日志等级有误" };
            }

            if (string.IsNullOrEmpty(model.Message))
            {
                return new ApiResultModel<string>() { message = "日志内容不能为空" };
            }
            if (string.IsNullOrEmpty(model.ModuleType))
            {
                return new ApiResultModel<string>() { message = "日志类型不能为空" };
            }

            if (string.IsNullOrEmpty(model.Ip))
            {
                model.Ip = GetIp();
            }
            #endregion

            try
            {
                await Task.Run(() =>
                {
                    _logService.Add(model.Level, model.UserName, model.Message, model.Exception, model.ObjectKey, model.ModuleType, model.Ip);
                });
            }
            catch (Exception ex)
            {
                return new ApiResultModel<string>(ApiResultCode.Fail, ex.Message);
            }

            return new ApiResultModel<string>(ApiResultCode.Success);
        }

        /// <summary>
        /// 查询列表（分页）
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="level">等级（Trace, Debug, Info, Warn, Error）</param>
        /// <param name="objectKey">操作对象</param>
        /// <param name="message">内容(模糊匹配)</param>
        /// <param name="moduleType">类型</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        [HttpGet("GetList")]
        public async Task<ApiResultModel<LogPagedResponse>> GetList(DateTime? startTime, DateTime? endTime, string level, string objectKey, string message, string moduleType, int pageIndex = 1, int pageSize = 20)
        {
            var result = new ApiResultModel<LogPagedResponse>();

            var data = await _logService.GetListAsync(startTime, endTime, level, objectKey, message, moduleType, pageIndex, pageSize);
            result.IsSuccessed(new LogPagedResponse { TotalCount = data.Total, DataList = data.List });
            return result;
        }

        /// <summary>
        /// Gets 请求IP
        /// </summary>
        private string GetIp()
        {
            var ip = Request.Headers["X-Forwarded-For"].FirstOrDefault(); // 解决 nginx、docker等 获取ip问题
            if (string.IsNullOrEmpty(ip))
            {
                ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString().Replace("::ffff:", string.Empty);
            }

            if (string.IsNullOrEmpty(ip))
            {
                ip = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString().Replace("::ffff:", string.Empty);
            }

            return ip;
        }
    }
}
