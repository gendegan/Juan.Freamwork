using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Juan.Core
{
    public static class DateHelper
    {
        // Methods
        public static string AgeToBirthday(this string age)
        {
            int num = Convert.ToInt32(DateTime.Now.Year.ToString()) - Convert.ToInt32(age);
            return (num.ToString() + "0101");
        }

        public static void BindControlAmFm(ListControl list)
        {
            list.Items.Clear();
            list.Items.Add("AM(上午)");
            list.Items.Add("FM(下午)");
        }

        public static void BindControlDate(ListControl drpYear, ListControl drpMonth, ListControl drpDay, DateTime dateTime)
        {
            IDisposable disposable;
            IEnumerator enumerator = drpYear.Items.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    ListItem current = (ListItem)enumerator.Current;
                    current.Selected = false;
                    int year = dateTime.Year;
                    if (current.Value.ToString() == year.ToString())
                    {
                        current.Selected = true;
                    }
                }
            }
            finally
            {
                disposable = enumerator as IDisposable;
                if (disposable !=null)
                {
                    disposable.Dispose();
                }
            }
            enumerator = drpMonth.Items.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    ListItem item2 = (ListItem)enumerator.Current;
                    item2.Selected = false;
                    int month = dateTime.Month;
                    if (item2.Value.ToString() == month.ToString())
                    {
                        item2.Selected = true;
                    }
                }
            }
            finally
            {
                disposable = enumerator as IDisposable;
                if (disposable !=null)
                {
                    disposable.Dispose();
                }
            }
            BindDay(drpDay, dateTime.Year, dateTime.Month);
            enumerator = drpDay.Items.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    ListItem item3 = (ListItem)enumerator.Current;
                    item3.Selected = false;
                    int day = dateTime.Day;
                    if (item3.Value.ToString() == day.ToString())
                    {
                        item3.Selected = true;
                    }
                }
            }
            finally
            {
                disposable = enumerator as IDisposable;
                if (disposable !=null)
                {
                    disposable.Dispose();
                }
            }
        }

        public static void BindDate(ListControl yearControl, ListControl monthControl, ListControl dayControl, int startYear, int endYear)
        {
            yearControl.BindYear(startYear, endYear);
            BindMonth(monthControl);
            BindDay(dayControl, Convert.ToInt16(yearControl.SelectedItem.Value), Convert.ToInt16(monthControl.SelectedItem.Value));
        }

        public static void BindDay(ListControl list, int year, int month)
        {
            list.Items.Clear();
            int num = DateTime.DaysInMonth(year, month);
            for (int i = 1; i <= num; i++)
            {
                string text = i.ToString();
                list.Items.Add(new ListItem(text, i.ToString()));
            }
        }

        public static void BindMonth(ListControl list)
        {
            list.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                string text = i.ToString();
                list.Items.Add(new ListItem(text, i.ToString()));
            }
        }

        public static void BindYear(this ListControl list, int startYear, int endYear)
        {
            list.Items.Clear();
            for (int i = startYear; i <= endYear; i++)
            {
                string text = i.ToString();
                list.Items.Add(new ListItem(text, i.ToString()));
                startYear++;
            }
        }

        public static string ChineseName(this DayOfWeek objDayOfWeek)
        {
            switch (objDayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "星期日";

                case DayOfWeek.Monday:
                    return "星期一";

                case DayOfWeek.Tuesday:
                    return "星期二";

                case DayOfWeek.Wednesday:
                    return "星期三";

                case DayOfWeek.Thursday:
                    return "星期四";

                case DayOfWeek.Friday:
                    return "星期五";

                case DayOfWeek.Saturday:
                    return "星期六";
            }
            return "星期几";
        }

        public static List<int> DateDiff(this DateTime DateTime1, DateTime DateTime2)
        {
            List<int> list2;
            try
            {
                List<int> list = new List<int>();
                TimeSpan span = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts = new TimeSpan(DateTime2.Ticks);
                TimeSpan span3 = span.Subtract(ts).Duration();
                list.Add(span3.Days);
                list.Add(span3.Hours);
                list.Add(span3.Seconds);
                list.Add(span3.Milliseconds);
                list2 = list;
            }
            catch (Exception exception)
            {
                throw new ArgumentException("公共层DateTimeHelper类DateDiff方法参数错误", exception);
            }
            return list2;
        }

        public static string DayOfWeekChinese(this DateTime dateTime)
        {
            return dateTime.DayOfWeek.ChineseName();
        }

        public static bool FormatDate(ref string dateTime)
        {
            dateTime = dateTime.Trim();
            try
            {
                Convert.ToDateTime((string)dateTime);
            }
            catch
            {
                return false;
            }
            if (dateTime.Length > 10)
            {
                dateTime = dateTime.Substring(0, 10);
            }
            if (dateTime.Length != 10)
            {
                string str = dateTime;
                string str2 = str.Substring(0, str.IndexOf("-"));
                str = str.Substring(str.IndexOf("-") + 1, (str.Length - str.IndexOf("-")) - 1);
                string str3 = str.Substring(0, str.IndexOf("-"));
                string str4 = str.Substring(str.IndexOf("-") + 1, (str.Length - str.IndexOf("-")) - 1);
                string[] textArray1 = new string[] { str2.PadLeft(4, '0'), "-", str3.PadLeft(2, '0'), "-", str4.PadLeft(2, '0') };
                dateTime = string.Concat(textArray1);
            }
            return true;
        }

        public static DateTime GetControlDateTime(DropDownList yearControl, DropDownList monthControl, DropDownList dayControl)
        {
            return new DateTime(int.Parse(yearControl.SelectedItem.Value), int.Parse(monthControl.SelectedItem.Value), int.Parse(dayControl.SelectedItem.Value));
        }

        public static string GetDatePath(this DateTime date)
        {
            string str = "";
            string str2 = "";
            str = str + DateTime.Now.Year + "/";
            str2 = "00" + DateTime.Now.Month;
            str = str + str2.Substring(str2.Length - 2, 2) + "/";
            str2 = "00" + DateTime.Now.Day;
            return (str + str2.Substring(str2.Length - 2, 2));
        }

        public static string GetDateString(this string splitString)
        {
            DateTime now = DateTime.Now;
            StringBuilder builder = new StringBuilder();
            builder.Append(now.Year.ToString("0000"));
            builder.Append(splitString);
            builder.Append(now.Month.ToString("00"));
            builder.Append(splitString);
            builder.Append(now.Day.ToString("00"));
            return builder.ToString();
        }

        public static List<string> GetDays(this DateTime time1, DateTime time2)
        {
            List<string> list = new List<string>();
            try
            {
                TimeSpan span = (TimeSpan)(time2 - time1);
                for (int i = 0; i <= span.Days; i++)
                {
                    try
                    {
                        string dateTime = time1.AddDays((double)i).ToShortDateString();
                        if (FormatDate(ref dateTime))
                        {
                            list.Add(dateTime);
                        }
                    }
                    catch (ArgumentException exception)
                    {
                        throw new ArgumentException("公共层TimeParser类GetDays方法参数错误", exception);
                    }
                }
            }
            catch (ArgumentException exception2)
            {
                throw new ArgumentException("公共层TimeParser类GetDays方法参数错误", exception2);
            }
            catch (ArithmeticException exception3)
            {
                throw new ArithmeticException("公共层TimeParser类GetDays方法类型转换错误", exception3);
            }
            return list;
        }

        public static int GetDiffDays(this DateTime DateTime1, DateTime DateTime2)
        {
            int num2;
            try
            {
                TimeSpan span = (TimeSpan)(DateTime1.Date - DateTime2.Date);
                num2 = Convert.ToInt32(span.TotalDays);
            }
            catch (Exception exception)
            {
                throw new ArgumentException("公共层DateTimeHelper类GetDiffDays方法参数错误", exception);
            }
            return num2;
        }



        public static string GetMonthQuarter(this int month)
        {
            switch (month)
            {
                case 1:
                case 2:
                case 3:
                    return "第一季度";

                case 4:
                case 5:
                case 6:
                    return "第二季度";

                case 7:
                case 8:
                case 9:
                    return "第三季度";

                case 10:
                case 11:
                case 12:
                    return "第四季度";
            }
            return "";
        }

        public static string GetTimeString(this string splitString)
        {
            DateTime now = DateTime.Now;
            StringBuilder builder = new StringBuilder();
            builder.Append(now.Year.ToString("0000"));
            builder.Append(splitString);
            builder.Append(now.Month.ToString("00"));
            builder.Append(splitString);
            builder.Append(now.Day.ToString("00"));
            builder.Append(splitString);
            builder.Append(now.Hour.ToString("00"));
            builder.Append(splitString);
            builder.Append(now.Minute.ToString("00"));
            builder.Append(splitString);
            builder.Append(now.Second.ToString("00"));
            return builder.ToString();
        }

        public static int GetWeekAmount(this int year)
        {
            DateTime time = new DateTime(year, 12, 0x1f);
            GregorianCalendar calendar = new GregorianCalendar();
            return calendar.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        public static int GetWeekAmount(this int year, DayOfWeek firstDayOfWeek)
        {
            DateTime time = new DateTime(year, 12, 0x1f);
            GregorianCalendar calendar = new GregorianCalendar();
            return calendar.GetWeekOfYear(time, CalendarWeekRule.FirstDay, firstDayOfWeek);
        }

        public static int GetWeekOfMonth(DateTime date)
        {
            int day = date.Day;
            DateTime time = date.AddDays((double)(1 - date.Day));
            int num2 = (time.DayOfWeek == DayOfWeek.Sunday) ? 7 : ((int)time.DayOfWeek);
            int num3 = 7 - (num2 - 1);
            int num4 = day - num3;
            num4 = (num4 > 0) ? num4 : 1;
            return (((((num4 % 7) == 0) ? ((num4 / 7) - 1) : (num4 / 7)) + 1) + ((day > num3) ? 1 : 0));
        }

        public static int GetWeekOfYear(this DateTime date)
        {
            return date.GetWeekOfYear(DayOfWeek.Monday);
        }

        public static int GetWeekOfYear(this DateTime date, DayOfWeek firstDayOfWeek)
        {
            GregorianCalendar calendar = new GregorianCalendar();
            return calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, firstDayOfWeek);
        }

        public static void GetWeekRange(this DateTime date, out DateTime startDate, out DateTime endDate)
        {
            date.GetWeekRange(DayOfWeek.Monday, out startDate, out endDate);
        }

        public static void GetWeekRange(this int yearWeekValue, out DateTime startDate, out DateTime endDate)
        {
            yearWeekValue.GetWeekRange(DayOfWeek.Monday, out startDate, out endDate);
        }

        public static void GetWeekRange(this DateTime date, DayOfWeek firstDayOfWeek, out DateTime startDate, out DateTime endDate)
        {
            int num = (int)(date.DayOfWeek - firstDayOfWeek);
            if (num < 0)
            {
                num = 7 + num;
            }
            startDate = date.AddDays((double)-num).ToString("yyyy-MM-dd 00:00:00").ToDateTime();
            endDate = date.AddDays((double)((7 - num) - 1)).ToString("yyyy-MM-dd 23:59:59").ToDateTime();
        }

        public static void GetWeekRange(this int yearWeekValue, DayOfWeek firstDayOfWeek, out DateTime startDate, out DateTime endDate)
        {
            yearWeekValue.ToString().Substring(4).ToInt().GetWeekRange(yearWeekValue.ToString().Substring(0, 4).ToInt(), firstDayOfWeek, out startDate, out endDate);
        }

        public static void GetWeekRange(this int weekOfYearValue, int year, out DateTime startDate, out DateTime endDate)
        {
            weekOfYearValue.GetWeekRange(year, DayOfWeek.Monday, out startDate, out endDate);
        }

        public static void GetWeekRange(this int weekOfYearValue, int year, DayOfWeek firstDayOfWeek, out DateTime startDate, out DateTime endDate)
        {
            DateTime time = new DateTime(year, 1, 1);
            time.AddDays((double)((weekOfYearValue - 1) * 7)).GetWeekRange(firstDayOfWeek, out startDate, out endDate);
        }

        public static DateTime GetWeekRangeFirst(this DateTime date)
        {
            return date.GetWeekRangeFirst(DayOfWeek.Monday);
        }

        public static DateTime GetWeekRangeFirst(this DateTime date, DayOfWeek firstDayOfWeek)
        {
            DateTime time;
            DateTime time2;
            date.GetWeekRange(firstDayOfWeek, out time, out time2);
            return time;
        }

        public static int GetYearMonthValueDiff(this int yearMonthEndValue, int yearMonthStartValue)
        {
            DateTime time = yearMonthEndValue.ToString().ToDateTime("yyyyMM");
            DateTime time2 = yearMonthStartValue.ToString().ToDateTime("yyyyMM");
            return ((12 * (time.Year - time2.Year)) + (time.Month - time2.Month));
        }

        public static List<string> GetYears(this string beginYear, string endYear)
        {
            List<string> list = new List<string>();
            try
            {
                int num = Convert.ToInt32(endYear) - Convert.ToInt32(beginYear);
                for (int i = 0; i <= num; i++)
                {
                    list.Add((Convert.ToInt32(beginYear) + i).ToString());
                }
            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException("公共层TimeParser方法GetYears参数错误", exception);
            }
            catch (ArithmeticException exception2)
            {
                throw new ArithmeticException("公共层TimeParser方法GetYears类型转换错误", exception2);
            }
            return list;
        }

        public static int GetYearWeekValue(this DateTime date)
        {
            return date.GetYearWeekValue(DayOfWeek.Monday);
        }

        public static int GetYearWeekValue(this DateTime date, DayOfWeek firstDayOfWeek)
        {
            DateTime weekRangeFirst = date.GetWeekRangeFirst(firstDayOfWeek);
            return (weekRangeFirst.Year + weekRangeFirst.GetWeekOfYear(firstDayOfWeek).ToString().PadLeft(3, '0')).ToInt();
        }

        public static int GetYearWeekValue(this DateTime date, out DateTime startDate, out DateTime endDate)
        {
            return date.GetYearWeekValue(DayOfWeek.Monday, out startDate, out endDate);
        }

        public static int GetYearWeekValue(this DateTime date, DayOfWeek firstDayOfWeek, out DateTime startDate, out DateTime endDate)
        {
            date.GetWeekRange(firstDayOfWeek, out startDate, out endDate);
            return (startDate.Year + startDate.GetWeekOfYear(firstDayOfWeek).ToString().PadLeft(3, '0')).ToInt();
        }

        public static int GetYearWeekValueDiff(this int yearWeekEndValue, int yearWeekStartValue, DayOfWeek firstDayOfWeek)
        {
            DateTime time;
            DateTime time2;
            DateTime time3;
            DateTime time4;
            yearWeekEndValue.GetWeekRange(firstDayOfWeek, out time, out time2);
            yearWeekStartValue.GetWeekRange(firstDayOfWeek, out time3, out time4);
            TimeSpan span = (TimeSpan)(time - time3);
            return (span.Days / 7);
        }

        public static string IsSpanYear(this string checkDate, int checkDays)
        {
            string[] textArray1 = new string[] { checkDate.Substring(0, 4), "-", checkDate.Substring(4, 2), "-", checkDate.Substring(6, 2) };
            string str = string.Concat(textArray1);
            string str2 = checkDate.Substring(0, 4) + "-01-01";
            if (Convert.ToDateTime(str).Subtract(Convert.ToDateTime(str2)).Days < checkDays)
            {
                return Convert.ToInt32((int)(Convert.ToInt32(checkDate.Substring(0, 4)) - 1)).ToString();
            }
            return string.Empty;
        }

        public static bool IsWeekday(this DateTime dateTime)
        {
            DayOfWeek[] array = new DayOfWeek[] { DayOfWeek.Monday };
            return array.Contains<DayOfWeek>(dateTime.DayOfWeek);
        }

        public static bool IsWeekend(this DateTime dateTime)
        {
            DayOfWeek[] weekArray1 = new DayOfWeek[2];
            weekArray1[0] = DayOfWeek.Saturday;
            DayOfWeek[] array = weekArray1;
            return array.Contains<DayOfWeek>(dateTime.DayOfWeek);
        }

        public static string MonthQuarter(this DateTime dateTime)
        {
            return dateTime.Month.GetMonthQuarter();
        }

        public static string SolarTerm(this DateTime dateTime)
        {
            string[] strArray = new string[] {
            "小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至", "小暑", "大暑", "立秋", "处暑",
            "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至"
         };
            int[] numArray = new int[] {
            0, 0x52d8, 0xa5e3, 0xf95c, 0x14d59, 0x1a206, 0x1f763, 0x24d89, 0x2a45d, 0x2fbdf, 0x353d8, 0x3ac35, 0x404af, 0x45d25, 0x4b553, 0x50d19,
            0x56446, 0x5bac6, 0x61087, 0x6658a, 0x6b9db, 0x70d90, 0x760cc, 0x7b3b6
         };
            double num = 31556925974.7;
            DateTime time2 = DateTime.Parse("1900-01-06 02:05:00").AddMilliseconds(num * (dateTime.Year - 0x76c));
            TimeSpan span = (TimeSpan)(dateTime - time2);
            double totalMinutes = span.TotalMinutes;
            double num3 = totalMinutes + 1440.0;
            for (int i = 0; i < strArray.Length; i++)
            {
                if ((totalMinutes <= numArray[i]) && (num3 > numArray[i]))
                {
                    return strArray[i];
                }
            }
            return "";
        }

        public static string ToDateShortText(this DateTime objDateTime, string format = "yyyy-MM-dd")
        {
            return objDateTime.ToString(format);
        }

        public static string ToDateText(this DateTime objDateTime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return objDateTime.ToString(format);
        }

        public static DateTime ToDateTime(this string dateValue)
        {
            return Convert.ToDateTime(dateValue);
        }

        public static DateTime ToDateTime(this string dateValue, string dateFormat)
        {
            return DateTime.ParseExact(dateValue, dateFormat, CultureInfo.CurrentCulture);
        }

        public static DateTime ToDateTimeNow(this string value)
        {
            if (value.IsNull())
            {
                return DateTime.Now;
            }
            try
            {
                return Convert.ToDateTime(value);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static string ToShortText(this DateTime objDateTime, string format = "yyyy-MM-dd")
        {
            return objDateTime.ToString(format);
        }

        public static string ToText(this DateTime objDateTime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return objDateTime.ToString(format);
        }

        public static string ToTimeChinese(this DateTime objDateTime, bool isSingleUnit = false)
        {
            return ((TimeSpan)(DateTime.Now - objDateTime)).ToTimeChinese(isSingleUnit);
        }

        public static string ToTimeChinese(this TimeSpan objTimeSpan, bool isSingleUnit = false)
        {
            StringBuilder builder = new StringBuilder();
            if (objTimeSpan.TotalSeconds == 0.0)
            {
                return "0秒";
            }
            if (objTimeSpan.Days > 0)
            {
                builder.AppendFormat("{0}天", Math.Abs(objTimeSpan.Days));
                if (isSingleUnit)
                {
                    return builder.ToString();
                }
            }
            if (objTimeSpan.Hours > 0)
            {
                builder.AppendFormat("{0}小时", Math.Abs(objTimeSpan.Hours));
                if (isSingleUnit)
                {
                    return builder.ToString();
                }
            }
            if (objTimeSpan.Minutes > 0)
            {
                builder.AppendFormat("{0}分", Math.Abs(objTimeSpan.Minutes));
                if (isSingleUnit)
                {
                    return builder.ToString();
                }
            }
            if (objTimeSpan.Seconds > 0)
            {
                builder.AppendFormat("{0}秒", Math.Abs(objTimeSpan.Seconds));
                if (isSingleUnit)
                {
                    return builder.ToString();
                }
            }
            return builder.ToString();
        }

        public static string ToTimeMinutesChinese(this int useMinutes, bool isSingleUnit = false)
        {
            return TimeSpan.FromMinutes((double)useMinutes).ToTimeChinese(false);
        }

        public static string ToTimeSecondsChinese(this int useSeconds, bool isSingleUnit = false)
        {
            return TimeSpan.FromSeconds((double)useSeconds).ToTimeChinese(false);
        }

        public static string ToUseMinutesChinese(this int useMinutes, bool isSingleUnit = false)
        {
            return TimeSpan.FromMinutes((double)useMinutes).ToUseTimeChinese(false);
        }

        public static string ToUseSecondsChinese(this int useSeconds, bool isSingleUnit = false)
        {
            return TimeSpan.FromSeconds((double)useSeconds).ToUseTimeChinese(false);
        }

        public static string ToUseTimeChinese(this DateTime objDateTime, bool isSingleUnit = false)
        {
            return ((TimeSpan)(DateTime.Now - objDateTime)).ToUseTimeChinese(isSingleUnit);
        }

        public static string ToUseTimeChinese(this TimeSpan objTimeSpan, bool isSingleUnit = false)
        {
            StringBuilder builder = new StringBuilder();
            if (objTimeSpan.TotalSeconds == 0.0)
            {
                return "刚刚";
            }
            if (objTimeSpan.Days > 0)
            {
                builder.AppendFormat("{0}天", Math.Abs(objTimeSpan.Days));
                if (isSingleUnit)
                {
                    return builder.ToString();
                }
            }
            if (objTimeSpan.Hours > 0)
            {
                builder.AppendFormat("{0}小时", Math.Abs(objTimeSpan.Hours));
                if (isSingleUnit)
                {
                    return builder.ToString();
                }
            }
            if (objTimeSpan.Minutes > 0)
            {
                builder.AppendFormat("{0}分", Math.Abs(objTimeSpan.Minutes));
                if (isSingleUnit)
                {
                    return builder.ToString();
                }
            }
            if (objTimeSpan.Seconds > 0)
            {
                builder.AppendFormat("{0}秒", Math.Abs(objTimeSpan.Seconds));
                if (isSingleUnit)
                {
                    return builder.ToString();
                }
            }
            return builder.ToString();
        }

        public static void WeekBetween(this DateTime dt, out DateTime startDate, out DateTime endDate)
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    startDate = dt;
                    endDate = dt.AddDays(6.0);
                    return;

                case DayOfWeek.Tuesday:
                    startDate = dt.AddDays(-1.0);
                    endDate = dt.AddDays(5.0);
                    return;

                case DayOfWeek.Wednesday:
                    startDate = dt.AddDays(-2.0);
                    endDate = dt.AddDays(4.0);
                    return;

                case DayOfWeek.Thursday:
                    startDate = dt.AddDays(-3.0);
                    endDate = dt.AddDays(3.0);
                    return;

                case DayOfWeek.Friday:
                    startDate = dt.AddDays(-4.0);
                    endDate = dt.AddDays(2.0);
                    return;

                case DayOfWeek.Saturday:
                    startDate = dt.AddDays(-5.0);
                    endDate = dt.AddDays(1.0);
                    return;
            }
            startDate = dt.AddDays(-6.0);
            endDate = dt;
        }
    }


}
