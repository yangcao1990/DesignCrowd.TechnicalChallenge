namespace DesignCrowd.TechnicalChallenge.Models
{
    /// <summary>
    /// Represents a public holiday on a relative day to another public holiday,
    /// e.g. Good Friday on the most recent Friday before Easter Sunday.
    /// </summary>
    public class RelativeDayOfWeekPublicHoliday : PublicHoliday
    {
        private readonly PublicHoliday _relatedPublicHoliday;
        private readonly bool _beforeRelatedPublicHoliday;
        private DayOfWeek _dayOfWeek;

        public RelativeDayOfWeekPublicHoliday(
            string name,
            PublicHoliday relatedPublicHoliday,
            bool beforeRelatedPublicHoliday,
            DayOfWeek dayOfWeek)
            : base(name)
        {
            _relatedPublicHoliday = relatedPublicHoliday;
            _beforeRelatedPublicHoliday = beforeRelatedPublicHoliday;
            _dayOfWeek = dayOfWeek;
        }

        public override DateTime GetDate(int calendarYear)
        {
            var relatedPublicHolidayDate = _relatedPublicHoliday.GetDate(calendarYear);

            var daysOffset = (int)_dayOfWeek - (int)relatedPublicHolidayDate.DayOfWeek;

            if (_beforeRelatedPublicHoliday && daysOffset > 0)
            {
                daysOffset -= 7;
            }
            else if (!_beforeRelatedPublicHoliday && daysOffset < 0)
            {
                daysOffset += 7;
            }

            return relatedPublicHolidayDate.AddDays(daysOffset);
        }

    }
}
