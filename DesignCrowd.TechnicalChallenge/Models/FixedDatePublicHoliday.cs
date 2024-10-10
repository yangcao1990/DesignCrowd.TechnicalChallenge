namespace DesignCrowd.TechnicalChallenge.Models
{
    /// <summary>
    /// Represents a public holiday on a fixed date, e.g. Anzac Day on April 25th every year.
    /// When the holiday falls on a weekend or another public holiday, if <see cref="_substituted"/>
    /// is True, it will move to the next non holiday weekday, otherwise it remains as-is.
    /// </summary>
    public class FixedDatePublicHoliday : PublicHoliday
    {
        private readonly int _holidayMonth;
        private readonly int _holidayDay;
        private readonly bool _substituted;

        public FixedDatePublicHoliday(string name, int holidayMonth, int holidayDay, bool substituted)
            : base(name)
        {
            _holidayMonth = holidayMonth;
            _holidayDay = holidayDay;
            _substituted = substituted;
        }

        public override DateTime GetDate(int calendarYear)
        {
            var dateString = $"{calendarYear}-{_holidayMonth}-{_holidayDay}";

            return ParseDate(dateString);
        }

        public override DateTime? GetAdditionalDate(int calendarYear, IEnumerable<PublicHoliday>? publicHolidays)
        {
            if (!_substituted)
            {
                return null;
            }

            var holidayDate = GetDate(calendarYear);

            var holidays = publicHolidays?.Select(holiday => holiday).ToList() ?? new List<PublicHoliday>();

            var others = holidays.Where(holiday => holiday.GetDate(calendarYear) != holidayDate).ToList();

            var additionalDate = holidayDate;

            if (holidayDate.DayOfWeek == DayOfWeek.Saturday)
            {
                additionalDate = holidayDate.AddDays(2);
            }
            else if (holidayDate.DayOfWeek == DayOfWeek.Sunday)
            {
                additionalDate = holidayDate.AddDays(1);
            }

            if (others.Any(holiday => holiday.GetDate(calendarYear) == additionalDate))
            {
                additionalDate = additionalDate.AddDays(1);
            }

            if (others.Any(holiday => holiday.GetDate(calendarYear) < holidayDate &&
                                      holiday.GetAdditionalDate(calendarYear, holidays) == additionalDate))
            {
                additionalDate = additionalDate.AddDays(1);
            }

            return additionalDate == holidayDate ? null : additionalDate;
        }

    }
}
