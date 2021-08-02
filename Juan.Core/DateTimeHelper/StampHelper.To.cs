using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 时间戳帮助类
    /// </summary>
    public static partial class StampHelper
    {



        /// <summary>
        ///  时间戳转日期 [毫秒数]
        /// </summary>
        /// <param name="Stamp">毫秒时间戳</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this Int64 Stamp)
        {
            //1970年01月01日00时00分00秒
            System.DateTime t = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0));
            System.TimeSpan span = new System.TimeSpan(Stamp * TimeSpan.TicksPerMillisecond);
            return t + span;
        }
        /// <summary>
        ///  时间戳转日期 [秒数]
        /// </summary>
        /// <param name="SecondStamp">秒时间戳</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this Int32 SecondStamp)
        {
            //1970年01月01日00时00分00秒
            System.DateTime t = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0));
            System.TimeSpan span = new System.TimeSpan(SecondStamp * TimeSpan.TicksPerSecond);
            return t + span;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SecondStamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeSecond(this Int64 SecondStamp)
        {
            //1970年01月01日00时00分00秒
            System.DateTime t = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0));
            System.TimeSpan span = new System.TimeSpan(SecondStamp * TimeSpan.TicksPerSecond);
            return t + span;
        }
        /// <summary>
        ///  日期时转间戳 [毫秒数]
        /// </summary>
        /// <param name="date">时间</param>
        /// <returns></returns>
        public static Int64 ToStamp(this DateTime date)
        {
            //1970年01月01日00时00分00秒
            System.DateTime t = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0));
            //与当前的时间差
            System.TimeSpan span = date - t;
            //取刻度数(以毫秒为刻度)
            return Convert.ToInt64(span.Ticks / TimeSpan.TicksPerMillisecond);
        }

        /// <summary>
        /// 日期时转间戳 [秒数]
        /// </summary>
        /// <param name="date">时间</param>
        /// <returns></returns>
        public static Int32 ToStampSecond(this DateTime date)
        {
            //1970年01月01日00时00分00秒
            System.DateTime t = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0));
            //与当前的时间差
            System.TimeSpan span = date - t;
            //取刻度数(以毫秒为刻度)
            return Convert.ToInt32(span.Ticks / TimeSpan.TicksPerSecond);
        }

        /// <summary>
        /// 日期时转间戳 [秒数]
        /// </summary>
        /// <param name="date">时间</param>
        /// <returns></returns>
        public static Int64 ToStampSecondLong(this DateTime date)
        {
            //1970年01月01日00时00分00秒
            System.DateTime t = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0));
            //与当前的时间差
            System.TimeSpan span = date - t;
            //取刻度数(以毫秒为刻度)
            return Convert.ToInt64(span.Ticks / TimeSpan.TicksPerSecond);
        }

        /// <summary>
        /// 日期时转间戳 [毫秒数]
        /// </summary>
        /// <param name="datetime">时间</param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Int64 ToStamp(this string datetime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            DateTime objDateTime = DateTime.ParseExact(datetime, format, System.Globalization.CultureInfo.CurrentCulture);
            return objDateTime.ToStamp();
        }
        /// <summary>
        /// 日期时转间戳 [秒数]
        /// </summary>
        /// <param name="datetime">时间</param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Int32 ToStampSecond(this string datetime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            DateTime objDateTime = DateTime.ParseExact(datetime, format, System.Globalization.CultureInfo.CurrentCulture);
            return objDateTime.ToStampSecond();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Int64 ToStampSecondLong(this string datetime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            DateTime objDateTime = DateTime.ParseExact(datetime, format, System.Globalization.CultureInfo.CurrentCulture);
            return objDateTime.ToStampSecondLong();
        }
    }

}
