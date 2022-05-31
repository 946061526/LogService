using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LogService.Tools
{
    /// <summary>
    /// 通用工具类
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// 获取时间字符串，默认格式yyyyMMddHHmmssfffff
        /// </summary>
        /// <param name="format">格式</param>
        /// <returns>string</returns>
        public static string GetTimeStr(string format = "yyyyMMddHHmmssfffff")
        {
            return DateTime.Now.ToString(format);
        }

        /// <summary>
        /// 获取Guid
        /// </summary>
        /// <returns>Guid</returns>
        public static string GetGuid()
        {
            var guid = Guid.NewGuid().ToString("N");
            return guid;
        }

        /// <summary>
        /// 获取4位随机数
        /// </summary>
        public static string GetRandom()
        {
            Random r = new Random(System.Environment.TickCount);
            int i = r.Next(1000, 9999);

            return i.ToString();
        }
        
        /// <summary>
         /// 获取随机字符串.
         /// </summary>
         /// <param name="n">长度.</param>
         /// <returns>.</returns>
        public static string GetRandStr(int n)
        {
            string str = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder sb = new StringBuilder();
            Random rd = new Random();
            for (int i = 0; i < n; i++)
            {
                sb.Append(str.Substring(rd.Next(0, str.Length), 1));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获得字符串中开始和结束字符串中间得值.
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="s">开始.</param>
        /// <param name="e">结束.</param>
        /// <returns>list.</returns>
        public static List<string> GetValues(string str, string s, string e)
        {
            List<string> list = new List<string>();
            Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            MatchCollection matches = rg.Matches(str);
            for (int i = 0; i < matches.Count; i++)
            {
                if (!list.Contains(matches[i].Value))
                {
                    list.Add(matches[i].Value);
                }
            }

            return list;
        }

        /// <summary>
        /// 把二进制，八进制，十六进制字符串向十进制数字的转换
        /// </summary>
        /// <param name="num">原数值</param>
        /// <param name="fromHex">原数值的进制（如:2,8,16）</param>
        /// <returns></returns>
        public static int ToHex10(string num, int fromHex = 8)
        {
            if (string.IsNullOrEmpty(num))
            {
                return -1;
            }

            return Convert.ToInt32(num, fromHex);
        }

        /// <summary>
        /// 十进制转化为2、8、16进制
        /// </summary>
        /// <param name="num">原数值</param>
        /// <param name="toHex">要转化的目标进制（如:2,8,16）</param>
        /// <returns></returns>
        public static string ToHex(int num, int toHex = 8)
        {
            if (toHex == 16)
            {
                return num.ToString("X2");
            }
            else
            {
                return Convert.ToString(num, toHex);
            }
        }

        /// <summary>
        /// 将当前时间转化为时间戳
        /// </summary>
        /// <returns>Unix时间戳格式</returns>
        public static long GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }

        /// <summary>
        /// 将DateTime时间转换为时间戳（13位带毫秒的Unix时间戳）.
        /// </summary>
        /// <param name="time">DateTime时间格式.</param>
        /// <returns>Unix时间戳格式.</returns>
        public static long GetTimestamp(DateTime time)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            long timeStamp = Convert.ToInt64((time - dateStart).TotalMilliseconds);
            return timeStamp;
        }

        /// <summary>
        ///  时间戳[10|13]转为C#格式时间
        /// </summary>
        public static DateTime StampToDateTime(string stamp)
        {
            if (stamp.Length != 10 || stamp.Length != 13)
            {
                return DateTime.Now;
            }

            try
            {
                DateTime startDateTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));
                if (stamp.Length == 10)
                {
                    startDateTime.AddSeconds(long.Parse(stamp));
                }

                return startDateTime.AddMilliseconds(long.Parse(stamp));
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
    }
}
