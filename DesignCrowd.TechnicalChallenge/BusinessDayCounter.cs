namespace DesignCrowd.TechnicalChallenge
{
    public class BusinessDayCounter
    {
        private static bool IsWeekday(DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
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
            if (firstDate >= secondDate)
            {
                return 0;
            }

            var startDate = firstDate.AddDays(1).Date;
            var endDate = secondDate.AddDays(-1).Date;

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

            if (firstDate >= secondDate)
            {
                return 0;
            }

            var startDate = firstDate.AddDays(1).Date;
            var endDate = secondDate.AddDays(-1).Date;

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
