using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 十进制转36进制
    /// </summary>
    public static class IntToHex36Helper
    {
        //不可修改
        private static List<char> _Hex36Digits = new List<char> { 't', 'o', 'q', 'd', '8', '7', 'n', 'a', '1', 'v', '3', 'w', 'l', 'k', 's', 'g', '9', '5', 'e', '4', 'p', '6', 'r', 'z', 'i', 'm', 'x', '2', 'y', 'u', 'f', 'b', 'j', 'h', 'c', '0' };
        /// <summary>
        /// 十进制转62进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IntToHex36(this UInt64 value)
        {
            string charValues = "";
            do
            {
                int RemainValue = (int)(value % 36);
                value = value / 36;
                char charValue = _Hex36Digits[RemainValue];
                charValues = charValue + charValues;

            } while (value > 0);
            return charValues;
        }

        /// <summary>
        /// 62进制转十进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UInt64 Hex36ToUInt64(this string value)
        {
            UInt64 valueInt64 = 0;
            char[] valueChars = value.ToCharArray().Reverse().ToArray();
            for (int i = 0; i < valueChars.Length; i++)
            {
                valueInt64 = valueInt64 + ((UInt32)_Hex36Digits.IndexOf(valueChars[i])) * Convert.ToUInt64(Math.Pow(36, i));
            }
            return valueInt64;
        }
        /// <summary>
        /// 十进制转62进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IntToHex36(this Int64 value)
        {
            return IntToHex36((UInt64)value);
        }
        /// <summary>
        /// 十进制转62进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IntToHex36(this Int32 value)
        {
            return IntToHex36((UInt64)value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IntToHex36(this UInt32 value)
        {
            return IntToHex36((UInt64)value);
        }

        /// <summary>
        ///  62进制转十进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int64 Hex36ToInt64(this string value)
        {
            return (Int64)Hex36ToUInt64(value);
        }
        /// <summary>
        ///  62进制转十进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int32 Hex36ToInt32(this string value)
        {
            return (Int32)Hex36ToUInt64(value);
        }
        /// <summary>
        ///  62进制转十进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UInt32 Hex36ToUInt32(this string value)
        {
            return (UInt32)Hex36ToUInt64(value);
        }
    }
}
