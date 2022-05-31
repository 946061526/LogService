using LogService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogService.Core.Repository
{
    public interface ILogRepository
    {
        /// <summary>
        /// 新增日志
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> AddAsync(SaveLogDto dto);

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
