using DesignCrowd.TechnicalChallenge.Models;

namespace DesignCrowd.TechnicalChallenge
{
    public class BusinessDayCounter
    {
        private static bool IsWeekday(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }

        private static bool FirstDateIsNotBeforeSecondDate(DateTime first, DateTime second)
        {
            return first >= second;
        }

        private static DateTime GetStartDate(DateTime date)
        {
            return date.AddDays(1).Date;
        }

        private static DateTime GetEndDate(DateTime date)
        {
            return date.AddDays(-1).Date;
        }

        /// <summary>
        /// Calculates the number of weekdays between firstDate and secondDate, excluding firstDate and secondDate
        /// <list type="bullet">
        ///     <item>
        ///         <description>Weekdays are Monday, Tuesday, Wednesday, Thursday, Friday.</description>
        ///     </item>
        ///     <item>
        ///         <description>The returned count <b>does not</b> include either firstDate or secondDate.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="firstDate">The date to start from, excl. from the count</param>
        /// <param name="secondDate">The date to stop, excl. from the count</param>
        /// <returns>
        /// Number of weekdays between firstDate and secondDate, excluding firstDate and secondDate;
        /// returns 0 if secondDate is equal to or before firstDate
        /// </returns>
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            if (FirstDateIsNotBeforeSecondDate(firstDate, secondDate))
            {
                return 0;
            }

            var startDate = GetStartDate(firstDate);
            var endDate = GetEndDate(secondDate);

            var weekdays = 0;

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (IsWeekday(date))
                {
                    weekdays++;
                }
            }

            return weekdays;
        }

        /// <summary>
        /// Calculates the number of business days between firstDate and secondDate, excluding firstDate and secondDate
        /// <list type="bullet">
        ///     <item>
        ///         <description>
        ///             Weekdays are Monday, Tuesday, Wednesday, Thursday, Friday, but excluding any dates which appear
        ///             in the <see cref="publicHolidays"/>
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>The returned count <b>does not</b> include either firstDate or secondDate.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="firstDate">The date to start from, excl. from the count</param>
        /// <param name="secondDate">The date to stop, excl. from the count</param>
        /// <param name="publicHolidays">A list of dates indicating public holidays</param>
        /// <returns>
        /// Number of business days between firstDate and secondDate, excluding firstDate and secondDate;
        /// returns 0 if secondDate is equal to or before firstDate
        /// </returns>
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            bool IsPublicHoliday(DateTime date)
            {
                if (publicHolidays == null)
                {
                    return false;
                }

                return publicHolidays.Any(holiday => holiday.Date == date.Date);
            }

            if (FirstDateIsNotBeforeSecondDate(firstDate, secondDate))
            {
                return 0;
            }

            var startDate = GetStartDate(firstDate);
            var endDate = GetEndDate(secondDate);

            var businessDays = 0;

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (IsWeekday(date) && !IsPublicHoliday(date))
                {
                    businessDays++;
                }
            }

            return businessDays;
        }

        /// <summary>
        /// Calculates the number of business days between firstDate and secondDate, excluding firstDate and secondDate
        /// <list type="bullet">
        ///     <item>
        ///         <description>
        ///             Weekdays are Monday, Tuesday, Wednesday, Thursday, Friday, but excluding any dates which appear
        ///             in the <see cref="PublicHoliday"/>
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>The returned count <b>does not</b> include either firstDate or secondDate.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="firstDate">The date to start from, excl. from the count</param>
        /// <param name="secondDate">The date to stop, excl. from the count</param>
        /// <param name="publicHolidays">A list of <see cref="PublicHoliday"/> indicating public holidays</param>
        /// <returns>
        /// Number of business days between firstDate and secondDate, excluding firstDate and secondDate;
        /// returns 0 if secondDate is equal to or before firstDate
        /// </returns>
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IEnumerable<PublicHoliday>? publicHolidays)
        {
            var holidays = publicHolidays?.Select(holiday => holiday).ToList() ?? new List<PublicHoliday>();

            bool IsPublicHoliday(DateTime date)
            {
                return holidays.Any(holiday => holiday.GetDate(date.Year) == date.Date ||
                                               holiday.GetAdditionalDate(date.Year, holidays) == date.Date ||
                                               holiday.GetDate(date.Year - 1) == date.Date ||
                                               holiday.GetAdditionalDate(date.Year - 1, holidays) == date.Date);
            }

            if (FirstDateIsNotBeforeSecondDate(firstDate, secondDate))
            {
                return 0;
            }

            var startDate = GetStartDate(firstDate);
            var endDate = GetEndDate(secondDate);

            var businessDays = 0;

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (IsWeekday(date) && !IsPublicHoliday(date))
                {
                    businessDays++;
                }
            }

            return businessDays;
        }

    }
}
