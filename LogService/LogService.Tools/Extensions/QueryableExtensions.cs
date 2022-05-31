using System;
using System.Linq;
using System.Linq.Expressions;

namespace LogService.Tools.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">IQueryable</param>
        /// <param name="skipCount">跳过条数</param>
        /// <param name="takeCount">读取条数</param>
        /// <returns></returns>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int takeCount)
        {
            return query.Skip(skipCount).Take(takeCount);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="query">IQueryable</param>
        /// <param name="skipCount">跳过条数</param>
        /// <param name="takeCount">读取条数</param>
        /// <returns></returns>
        public static TQueryable PageBy<T, TQueryable>(this TQueryable query, int skipCount, int takeCount)
            where TQueryable : IQueryable<T>
        {
            return (TQueryable)query.Skip(skipCount).Take(takeCount);
        }

        /// <summary>
        /// 条件过滤
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">IQueryable</param>
        /// <param name="condition">条件</param>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition ? query.Where(predicate) : query;
        }

        /// <summary>
        /// 条件过滤
        /// </summary>
        /// <param name="query">IQueryable</param>
        /// <param name="condition">条件</param>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public static TQueryable WhereIf<T, TQueryable>(this TQueryable query, bool condition, Expression<Func<T, bool>> predicate)
            where TQueryable : IQueryable<T>
        {
            return condition ? (TQueryable)query.Where(predicate) : query;
        }

        /// <summary>
        /// 条件过滤
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">IQueryable</param>
        /// <param name="condition">条件</param>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate)
        {
            return condition ? query.Where(predicate) : query;
        }

        /// <summary>
        /// 条件过滤
        /// </summary>
        /// <param name="query">IQueryable</param>
        /// <param name="condition">条件</param>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public static TQueryable WhereIf<T, TQueryable>(this TQueryable query, bool condition, Expression<Func<T, int, bool>> predicate)
            where TQueryable : IQueryable<T>
        {
            return condition ? (TQueryable)query.Where(predicate) : query;
        }
    }
}
