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
        /// 时间戳(从1970年01月01日00时00分00秒开始的毫秒数)
        /// </summary>
        public static Int64 Stamp
        {
            get
            {
                //1970年01月01日00时00分00秒
                System.DateTime startDate = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0));
                //与当前的时间差
                System.TimeSpan span = System.DateTime.Now - startDate;
                //取刻度数(以毫秒为刻度)
                return Convert.ToInt64(span.Ticks / TimeSpan.TicksPerMillisecond);
            }
        }
        /// <summary>
        /// 时间戳(从1970年01月01日00时00分00秒开始的秒数)
        /// </summary>
        public static Int32 StampSecond
        {
            get
            {
                //1970年01月01日00时00分00秒
                System.DateTime startDate = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0));
                //与当前的时间差
                System.TimeSpan span = System.DateTime.Now - startDate;
                //取刻度数(以毫秒为刻度)
                return Convert.ToInt32(span.Ticks / TimeSpan.TicksPerSecond);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static Int64 StampSecondLong
        {
            get
            {
                //1970年01月01日00时00分00秒
                System.DateTime startDate = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0));
                //与当前的时间差
                System.TimeSpan span = System.DateTime.Now - startDate;
                //取刻度数(以毫秒为刻度)
                return Convert.ToInt64(span.Ticks / TimeSpan.TicksPerSecond);
            }
        }

    }
}

