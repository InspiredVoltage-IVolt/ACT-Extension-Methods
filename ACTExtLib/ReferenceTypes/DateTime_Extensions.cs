namespace ACT.Core.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>Convert from UNIX time to Standard Time</summary>
        /// <param name="unixTime">Unix Time Seconds Since 1/1/1970 0:0:0</param>
        /// <returns>DateTime</returns>
        /// <seealso cref="T:System.DateTime" />
        public static DateTime FromUnixTime(this ulong unixTime) => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTime);

        /// <summary>Converts the DateTime to UNIX TIME (1/1/1970 0:0:0)</summary>
        /// <param name="date">Date to Convert</param>
        /// <returns></returns>
        public static ulong ToUnixTime(this DateTime date)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToUInt64((date - dateTime).TotalSeconds);
        }

        /// <summary>Calculate total seconds from the 2 dates</summary>
        /// <param name="Date1">Date1</param>
        /// <param name="Date2">Date2</param>
        /// <returns></returns>
        public static int TotalSeconds(this DateTime Date1, DateTime Date2) => ((IEnumerable<string>)(Date1 - Date2).TotalSeconds.ToString().SplitString(".", StringSplitOptions.RemoveEmptyEntries)).First<string>().ToIntFast();

        public static bool IsBefore(this DateTime Date1, DateTime Date2) => (Date1 - Date2).TotalSeconds < 0.0;

        /// <summary>Returns the Total Number Of Months (Always Positive)</summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Int Number Of Monthsnetflix</returns>
        public static int TotalMonths(this DateTime start, DateTime end) => Math.Abs(start.Year * 12 + start.Month - (end.Year * 12 + end.Month));

        public static int TotalAgeYears(this DateTime DOB) => DOB.ToAge().years;

        public static (int years, int months, int days) ToAge(this DateTime DOB)
        {
            DateTime today = DateTime.Today;
            int num1 = today.Month - DOB.Month;
            int num2 = today.Year - DOB.Year;
            if (today.Day < DOB.Day)
            {
                --num1;
            }

            if (num1 < 0)
            {
                --num2;
                num1 += 12;
            }
            int days = (today - DOB.AddMonths(num2 * 12 + num1)).Days;
            return (num2, num1, days);
        }

        public static int CalculateAge(this DateTime BDate, DateTime? EndDate = null)
        {
            if (!EndDate.HasValue)
            {
                EndDate = new DateTime?(DateTime.Today);
            }

            int num = EndDate.Value.Year - BDate.Year;
            if (BDate > EndDate.Value.AddYears(-num))
            {
                --num;
            }

            return num;
        }
    }



}
