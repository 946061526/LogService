using LogService.Core.Database;
using LogService.Core.Domain;
using LogService.Dto;
using LogService.Tools.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogService.Core.Repository
{
    /// <summary>
    /// 日志仓储
    /// </summary>
    public class LogRepository : ILogRepository
    {
        private readonly IBaseDbContext _db;
        public LogRepository(IBaseDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// 新增日志
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(SaveLogDto dto)
        {
            var entity = new LogEntity
            {
                Id = dto.Id,
                UserName = dto.UserName,
                Message = dto.Message,
                Exception = dto.Exception,
                Level = dto.Level,
                ObjectKey = dto.ObjectKey,
                ModuleType = dto.ModuleType,
                Ip = dto.Ip,
                LogTime = dto.LogTime,
                Timestamp = dto.Timestamp,
            };

            await _db.Log.AddAsync(entity);
            return await _db.SaveChangesAsync() > 0;
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
            var query = _db.Log.AsNoTracking();
            query = query.Where(x => x.LogTime >= startTime && x.LogTime < endTime);
            query = query.WhereIf(!string.IsNullOrEmpty(level), x => x.Level.ToLower() == level.ToLower());
            query = query.WhereIf(!string.IsNullOrEmpty(objectKey), x => x.ObjectKey.ToLower().Contains(objectKey.ToLower()));
            query = query.WhereIf(!string.IsNullOrEmpty(message), x => x.Message.ToLower().Contains(message.ToLower()));
            query = query.WhereIf(!string.IsNullOrEmpty(moduleType), x => x.ModuleType.ToLower().Equals(moduleType.ToLower()));

            var totalCount = await query.CountAsync();

            var skip = pageSize * (pageIndex - 1);
            if (skip > totalCount)
            {
                return (totalCount, new List<LogResponse>());
            }
            query = query.OrderByDescending(x => x.LogTime).PageBy(skip, pageSize);

            var list = await query.Select(a => new LogResponse
            {
                Id = a.Id,
                UserName = a.UserName,
                Message = a.Message,
                Exception = a.Exception,
                ObjectKey = a.ObjectKey,
                ModuleType = a.ModuleType,
                Level = a.Level,
                Ip = a.Ip,
                LogTime = a.LogTime,
                Timestamp = a.Timestamp,
            }).ToListAsync();

            return (totalCount, list);
        }
    }
}
