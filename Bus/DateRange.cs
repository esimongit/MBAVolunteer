using System;
using System.Web;
using System.Web.UI;
using NQN.DB;


/// <summary>
/// Summary description for DateRange
/// </summary>
namespace NQN.Bus
{
    public class DateRange
    {
        public enum Quarter
        {
            First = 1,
            Second = 2,
            Third = 3,
            Fourth = 4
        }

        public enum Month
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }
        public enum Week
        {
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6,
            Sunday = 0
        }

        public class DateUtilities
        {
            #region Quarters

            public static DateTime GetStartOfQuarter(int Year, int Qtr)
            {
                int q1month = 1;
                try
                {
                    q1month = Convert.ToInt32(StaticFieldsObject.StaticValue("FirstQuarter"));
                }
                catch { }
               
                int mo = q1month + 3 * (Qtr -1);
                if (mo > 12)
                {
                    mo -=  12;
                    Year += 1;
                }
                return new DateTime(Year, mo, 1, 0, 0, 0, 0);
               
            }

            public static DateTime GetEndOfQuarter(int Year, int Qtr)
            {
                return DateUtilities.GetStartOfQuarter(Year, Qtr).AddMonths(3).AddSeconds(-1);
            }

           
           
            #endregion

            #region Weeks
            public static DateTime GetStartOfWeek(DateTime dt)
            {
                /* Monday is the first day of the week */
                if (dt.DayOfWeek.GetHashCode() == (int)Week.Sunday)
                {
                    return dt.AddDays((double)(-6));
                }
                else
                {
                    return dt.AddDays((double)(1 - dt.DayOfWeek.GetHashCode()));
                }
            }

            public static DateTime GetEndOfWeek(DateTime dt)
            {
                /* Sunday is the last day of the week */
                if (dt.DayOfWeek.GetHashCode() == (int)Week.Sunday)
                {
                    return dt.AddDays((double)0);
                }
                else
                {
                    return dt.AddDays((double)(7 - dt.DayOfWeek.GetHashCode()));
                }
            }
            public static DateTime GetStartOfLastWeek()
            {
                int DaysToSubtract = (int)DateTime.Now.DayOfWeek + 7;
                DateTime dt =
                  DateTime.Now.Subtract(System.TimeSpan.FromDays(DaysToSubtract));
                return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
            }

            public static DateTime GetEndOfLastWeek()
            {
                DateTime dt = GetStartOfLastWeek().AddDays(6);
                return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
            }

            public static DateTime GetStartOfCurrentWeek()
            {
                int DaysToSubtract = (int)DateTime.Now.DayOfWeek;
                DateTime dt =
                  DateTime.Now.Subtract(System.TimeSpan.FromDays(DaysToSubtract));
                return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
            }

            public static DateTime GetEndOfCurrentWeek()
            {
                DateTime dt = GetStartOfCurrentWeek().AddDays(6);
                return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
            }
            #endregion

            #region Months

            public static DateTime GetStartOfMonth(int Year, Month Month)
            {
                return new DateTime(Year, (int)Month, 1, 0, 0, 0, 0);
            }

            public static DateTime GetEndOfMonth(int Year, Month Month)
            {
                return new DateTime(Year, (int)Month,
                   DateTime.DaysInMonth(Year, (int)Month), 23, 59, 59, 999);
            }

            public static DateTime GetStartOfLastMonth()
            {
                if (DateTime.Now.Month == 1)
                    return GetStartOfMonth(DateTime.Now.Year - 1, (Month)12);
                else
                    return GetStartOfMonth(DateTime.Now.Year, (Month)DateTime.Now.Month - 1);
            }

            public static DateTime GetEndOfLastMonth()
            {
                if (DateTime.Now.Month == 1)
                    return GetEndOfMonth(DateTime.Now.Year - 1, (Month)12);
                else
                    return GetEndOfMonth(DateTime.Now.Year, (Month)DateTime.Now.Month - 1);
            }

            public static DateTime GetStartOfCurrentMonth()
            {
                return GetStartOfMonth(DateTime.Now.Year, (Month)DateTime.Now.Month);
            }

            public static DateTime GetEndOfCurrentMonth()
            {
                return GetEndOfMonth(DateTime.Now.Year, (Month)DateTime.Now.Month);
            }
            #endregion

            #region Years
            public static DateTime GetStartOfYear(int Year)
            {
                return new DateTime(Year, 1, 1, 0, 0, 0, 0);
            }

            public static DateTime GetEndOfYear(int Year)
            {
                return new DateTime(Year, 12,
                  DateTime.DaysInMonth(Year, 12), 23, 59, 59, 999);
            }

            public static DateTime GetStartOfLastYear()
            {
                return GetStartOfYear(DateTime.Now.Year - 1);
            }

            public static DateTime GetEndOfLastYear()
            {
                return GetEndOfYear(DateTime.Now.Year - 1);
            }

            public static DateTime GetStartOfCurrentYear()
            {
                return GetStartOfYear(DateTime.Now.Year);
            }

            public static DateTime GetEndOfCurrentYear()
            {
                return GetEndOfYear(DateTime.Now.Year);
            }
            #endregion

            #region Days
            public static DateTime GetStartOfDay(DateTime date)
            {
                return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
            }

            public static DateTime GetEndOfDay(DateTime date)
            {
                return new DateTime(date.Year, date.Month,
                                     date.Day, 23, 59, 59, 999);
            }
            #endregion
            #region SemiYear
            public static DateTime GetStartOfSemiYear(int Year, int interval)
            {
                int mon;
                if (interval == 0)
                {
                    mon = 1;
                }
                else
                {
                    mon = 7;
                }
                return new DateTime(Year, mon, 1, 0, 0, 0, 0);
            }
            public static DateTime GetEndOfSemiYear(int Year, int interval)
            {
                int mon;
                if (interval == 0)
                {
                    mon = 6;
                }
                else
                {
                    mon = 12;
                }
                return new DateTime(Year, mon, DateTime.DaysInMonth(Year, mon), 0, 0, 0, 0);
            }
            #endregion

            
        }
        public static bool Validate(string dateString)
        {
            bool valid = false;
            try
            {
                DateTime dt = DateTime.Parse(dateString);
                if (dt > DateTime.Parse("1/1/1990") && dt < DateTime.Parse("12/31/2050")) valid = true;
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

    }
}
