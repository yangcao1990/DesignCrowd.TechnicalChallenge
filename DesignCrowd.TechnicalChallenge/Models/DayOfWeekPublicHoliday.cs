namespace DesignCrowd.TechnicalChallenge.Models
{
    /// <summary>
    /// Represents a public holiday on a certain occurrence of a certain day in a month,
    /// e.g. Queen's Birthday on the second Monday in June every year.
    /// </summary>
    public class DayOfWeekPublicHoliday : PublicHoliday
    {
        private readonly int _holidayMonth;
        private readonly int _occurrenceOfDay;
        private readonly DayOfWeek _dayOfWeek;

        public DayOfWeekPublicHoliday(string name, int holidayMonth, int occurrenceOfDay, DayOfWeek dayOfWeek)
            : base(name)
        {
            _holidayMonth = holidayMonth;
            _occurrenceOfDay = occurrenceOfDay;
            _dayOfWeek = dayOfWeek;
        }

        public override DateTime GetDate(int calendarYear)
        {
            var firstDayOfMonth = ParseDate($"{calendarYear}-{_holidayMonth}-{1}");

            var daysOffset = (int)_dayOfWeek - (int)firstDayOfMonth.DayOfWeek;

            if (daysOffset < 0)
            {
                daysOffset += 7;
            }

            daysOffset += (_occurrenceOfDay - 1) * 7;

            var holidayDate = firstDayOfMonth.AddDays(daysOffset);

            if (holidayDate.Month != _holidayMonth)
            {
                throw new ArgumentException($"{holidayDate:yyyy-M-d} falls outside the expected month of {_holidayMonth}");
            }

            return holidayDate;
        }

    }
}
