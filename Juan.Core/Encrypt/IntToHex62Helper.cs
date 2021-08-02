using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 十进制转62进制
    /// </summary>
    public static class IntToHex62Helper
    {
        //不可修改
        private static List<char> _Hex62Digits = new List<char> { 'x', 'A', '1', 'Q', 'L', 'M', 'H', 'k', '5', '3', 'K', 'r', 'l', 'P', 'c', 'B', 'g', 'G', 'O', 'y', 'E', 'v', 'f', 'Z', '0', 's', 'D', 'X', 't', '6', 'j', 'n', 'w', 'm', '9', 'F', 'T', 'q', 'i', 'u', '8', 'p', 'a', 'V', 'S', 'h', 'I', '7', 'z', 'o', 'N', 'W', 'U', 'b', '4', '2', 'R', 'Y', 'd', 'C', 'J', 'e' };

        /// <summary>
        /// 十进制转62进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IntToHex62(this UInt64 value)
        {
            string charValues = "";
            do
            {
                int RemainValue = (int)(value % 62);
                value = value / 62;
                char charValue = _Hex62Digits[RemainValue];
                charValues = charValue + charValues;

            } while (value > 0);
            return charValues;
        }

        /// <summary>
        /// 62进制转十进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UInt64 Hex62ToUInt64(this string value)
        {
            UInt64 valueInt64 = 0;
            char[] valueChars = value.ToCharArray().Reverse().ToArray();
            for (int i = 0; i < valueChars.Length; i++)
            {
                valueInt64 = valueInt64 + ((UInt32)_Hex62Digits.IndexOf(valueChars[i])) * Convert.ToUInt64(Math.Pow(62, i));
            }
            return valueInt64;
        }
        /// <summary>
        /// 十进制转62进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IntToHex62(this Int64 value)
        {
            return IntToHex62((UInt64)value);
        }
        /// <summary>
        /// 十进制转62进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IntToHex62(this  Int32 value)
        {
            return IntToHex62((UInt64)value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IntToHex62(this  UInt32 value)
        {
            return IntToHex62((UInt64)value);
        }

        /// <summary>
        ///  62进制转十进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int64 Hex62ToInt64(this string value)
        {
            return (Int64)Hex62ToUInt64(value);
        }
        /// <summary>
        ///  62进制转十进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int32 Hex62ToInt32(this string value)
        {
            return (Int32)Hex62ToUInt64(value);
        }
        /// <summary>
        ///  62进制转十进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UInt32 Hex62ToUInt32(this string value)
        {
            return (UInt32)Hex62ToUInt64(value);
        }
    }
}
