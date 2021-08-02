using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// 随机数帮助类
    /// </summary>
    public static partial class RandomHelper
    {

        private static string _LowerChar = "abcdefghijklmnopqrstuvwxyz";
        private static string _UpperChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string _NumberChar = "0123456789";

        #region 获取种子
        /// <summary>
        /// 使用RNGCryptoServiceProvider 做种，可以在一秒内产生的随机数重复率非常
        /// 的低，对于以往使用时间做种的方法是个升级
        /// </summary>
        /// <returns></returns>
        public static int GetNewSeed()
        #endregion
        {
            byte[] rndBytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(rndBytes);
            return BitConverter.ToInt32(rndBytes, 0);
        }


        #region 取得指定范围内的数字随几数
        /// <summary>
        /// 取得指定范围内的随几数
        /// </summary>
        /// <param name="startNumber">下限数</param>
        /// <param name="endNumber">上限数</param>
        /// <returns>int</returns>
        #endregion
        public static int GetRandomNumber(int startNumber, int endNumber)
        {

            Random objRandom = new Random(GetNewSeed());
            int r = objRandom.Next(startNumber, endNumber);
            return r;
        }


        #region 获取指定 ASCII 范围内的随机字符串
        /// <summary>
        /// 获取指定 ASCII 范围内的随机字符串
        /// </summary>
        /// <param name="resultLength">结果字符串长度</param>
        /// <param name="startNumber"> 开始的ASCII值 如（33－125）中的 33</param>
        /// <param name="endNumber"> 结束的ASCII值 如（33－125）中的 125</param>
        /// <returns></returns>
        #endregion
        public static string GetRandomStringByASCII(int resultLength, int startNumber, int endNumber)
        {
            System.Random objRandom = new System.Random(GetNewSeed());
            string result = null;
            for (int i = 0; i < resultLength; i++)
            {
                result += (char)objRandom.Next(startNumber, endNumber);
            }
            return result;
        }


        #region 从指定字符串中抽取指定长度的随机字符串
        /// <summary>
        /// 从指定字符串中抽取指定长度的随机字符串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="resultLength">待获取随机字符串长度</param>
        /// <returns></returns>
        #endregion
        private static string GetRandomString(string source, int resultLength)
        {
            System.Random objRandom = new System.Random(GetNewSeed());
            string result = null;
            for (int i = 0; i < resultLength; i++)
            {
                result += source.Substring(objRandom.Next(0, source.Length - 1), 1);
            }
            return result;
        }


        #region 获取指定长度随机的数字字符串
        /// <summary>
        /// 获取指定长度随机的数字字符串
        /// </summary>
        /// <param name="resultLength">待获取随机字符串长度</param>
        /// <returns></returns>
        #endregion
        public static string GetRandomNumberString(int resultLength)
        {
            return GetRandomString(_NumberChar, resultLength);
        }


        #region 获取指定长度随机的字母字符串（包含大小写字母）
        /// <summary>
        /// 获取指定长度随机的字母字符串（包含大小写字母）
        /// </summary>
        /// <param name="resultLength">待获取随机字符串长度</param>
        /// <returns></returns>
        #endregion
        public static string GetRandomLetterString(int resultLength)
        {
            return GetRandomString(_LowerChar + _UpperChar, resultLength);
        }


        #region 获取指定长度随机的字母＋数字混和字符串（包含大小写字母）
        /// <summary>
        /// 获取指定长度随机的字母＋数字混和字符串（包含大小写字母）
        /// </summary>
        /// <param name="resultLength">待获取随机字符串长度</param>
        /// <returns></returns>
        #endregion
        public static string GetRandomMixString(int resultLength)
        {
            return GetRandomString(_LowerChar + _UpperChar + _NumberChar, resultLength);
        }

        /// <summary>
        /// 获取整形随机数组
        /// </summary>
        /// <param name="len">数组长度</param>
        /// <param name="minValue">随机数下界</param>
        /// <param name="maxValue">随机数上界</param>
        /// <returns></returns>
        public static List<int> GetRanIntList(int len, int minValue, int maxValue)
        {
            Random r = new Random();
            List<int> list = new List<int>();

            if (len > (maxValue - minValue))
            {
                for (int i = minValue; i < maxValue; i++)
                {
                    list.Add(i);
                }
            }
            else
            {
                while (list.Count < len)
                {
                    int i = r.Next(minValue, maxValue);
                    int count = 0;
                    foreach (int j in list)
                    {
                        if (i == j)
                        {
                            count++;
                        }
                    }
                    if (count == 0)
                    {
                        list.Add(i);
                    }
                }
                list.Sort();
            }
            return list;
        }

        /// <summary>
        /// 随机取得字符串默认间隔逗号,
        /// </summary>
        /// <param name="RandomString"></param>
        /// <returns></returns>
        public static string GetRandomSwitchString(this string RandomString)
        {
            return GetRandomSwitchString(RandomString, ',');
        }
        /// <summary>
        /// 随机取得字符串
        /// </summary>
        /// <param name="RandomString"></param>
        /// <param name="trimChars"></param>
        /// <returns></returns>
        public static string GetRandomSwitchString(this string RandomString, params char[] trimChars)
        {
            if (string.IsNullOrWhiteSpace(RandomString))
            {
                return RandomString;
            }
            RandomString = RandomString.Trim(trimChars);
            string[] RandomArr = RandomString.Split(trimChars, StringSplitOptions.RemoveEmptyEntries);
            if (RandomArr.Length == 1)
            {
                return RandomString;
            }
            else
            {
                int i = GetRandomNumber(0, RandomArr.Length);
                return RandomArr[i];
            }

        }

        /// <summary>
        /// AB随机
        /// </summary>
        /// <param name="A">A的概率</param>
        /// <param name="B">B的概率</param>
        /// <returns></returns>
        public static string GetRandomAB(int A, int B)
        {
            int total = A + B;
            int randomIndex = RandomHelper.GetRandomNumber(0, total);
            if (randomIndex < A)
            {
                return "A";
            }
            return "B";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="probabilityDir"></param>
        /// <returns></returns>
        public static Key ToProbabilityValue<Key>(this IDictionary<Key, int> probabilityDir)
        {
            if (probabilityDir.Count == 0)
            {
                throw new Exception("输入的概率字典不能为空");
            }

            int probabilityTotal = probabilityDir.Sum(s => s.Value);
            int randomIndex = RandomHelper.GetRandomNumber(0, probabilityTotal);
            int startWeight = 0;
            int endWeight = 0;
            foreach (var item in probabilityDir)
            {
                endWeight = startWeight + item.Value;
                if (startWeight <= randomIndex && randomIndex < endWeight)
                {
                    return item.Key;
                }
                startWeight = endWeight;
            }
            return probabilityDir.FirstOrDefault().Key;

        }

        /// <summary>
        /// 算出概念选中对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="probabilityList"></param>
        /// <param name="probabilityValue"></param>
        /// <returns></returns>
        public static T ToProbabilityValue<T>(this IEnumerable<T> probabilityList, Func<T, int> probabilityValue)
        {
            if (probabilityList.Count() == 0)
            {
                throw new Exception("输入的概率列表不能为空");
            }
            int probabilityTotal = 0;
            foreach (var item in probabilityList)
            {
                probabilityTotal = probabilityTotal + probabilityValue(item);
            }
            int randomIndex = RandomHelper.GetRandomNumber(0, probabilityTotal);
            int startWeight = 0;
            int endWeight = 0;
            foreach (var item in probabilityList)
            {
                endWeight = startWeight + probabilityValue(item);
                if (startWeight <= randomIndex && randomIndex < endWeight)
                {
                    return item;
                }
                startWeight = endWeight;
            }
            return probabilityList.FirstOrDefault();

        }


    }
}
