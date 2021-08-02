using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class NeatlyCodeHelper
    {


        /// <summary>
        /// 自增整齐值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="leftWidth"></param>
        /// <returns></returns>
        public static long AddNeatly(this long value, int leftWidth)
        {
            int Length = value.ToString().Length;
            long leftWidthValue = long.Parse(value.ToString().Substring(0, leftWidth));
            leftWidthValue++;
            value = long.Parse(leftWidthValue.ToString().PadRight(Length, '0'));
            return value;
        }
        /// <summary>
        /// 获取整齐值区间
        /// </summary>
        /// <param name="value"></param>
        /// <param name="leftWidth"></param>
        /// <param name="neatlyWidth"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public static void BetweenNeatly(this long value, int leftWidth, int neatlyWidth, out long minValue, out long maxValue)
        {
            int Length = value.ToString().Length;
            long forwardValue = long.Parse(value.ToString().Substring(0, leftWidth - neatlyWidth));
            minValue = long.Parse((long.Parse(forwardValue.ToString().PadRight(leftWidth, '0')) + 1).ToString().PadRight(Length, '0'));
            maxValue = long.Parse((long.Parse(forwardValue.ToString().PadRight(leftWidth, '9'))).ToString().PadRight(Length, '0'));
        }

    }
}
