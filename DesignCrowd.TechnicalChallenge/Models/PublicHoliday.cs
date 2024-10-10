using System.Globalization;

namespace DesignCrowd.TechnicalChallenge.Models
{
    public abstract class PublicHoliday
    {
        public string Name { get; }

        protected PublicHoliday(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Get the date of the public holiday for the given calendar year. This is the as-is date without any adjustments
        /// </summary>
        /// <param name="calendarYear">The calendar year of the public holiday</param>
        /// <returns>The date of the public holiday as-is without any adjustments</returns>
        public abstract DateTime GetDate(int calendarYear);

        /// <summary>
        /// Get the additional date of the public holiday. This normally happens when a public holiday
        /// is adjusted to the next non public holiday weekdays when it falls on a weekend or another public holiday.
        /// This returns NULL if the public holiday is not applicable for weekdays adjustments, e.g. Anzac Day.
        /// </summary>
        /// <param name="calendarYear">The calendar year of the public holiday</param>
        /// <param name="publicHolidays">All public holidays which may conflict with this public holiday</param>
        /// <returns>The additional date of the public holiday after adjustments, or NULL if the holiday is not applicable for adjustment</returns>
        public virtual DateTime? GetAdditionalDate(int calendarYear, IEnumerable<PublicHoliday>? publicHolidays) => null;

        protected static DateTime ParseDate(string dateString)
        {
            const string format = "yyyy-M-d";

            if (!DateTime.TryParseExact(dateString, format, null, DateTimeStyles.None, out var date))
            {
                throw new ArgumentException($"{dateString} is not a valid date in format of {format}");
            }

            return date;
        }

    }
}
