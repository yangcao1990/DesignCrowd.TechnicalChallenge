using DesignCrowd.TechnicalChallenge.Models;

namespace DesignCrowd.TechnicalChallenge.Tests
{
    public class DayOfWeekPublicHolidayTests
    {
        [Fact]
        public void GetDate_InvalidOccurrenceOfDay_ThrowsArgumentException()
        {
            var holiday = new DayOfWeekPublicHoliday("Queen's Birthday", 6, 10, DayOfWeek.Monday);

            var exception = Assert.Throws<ArgumentException>(() => holiday.GetDate(2021));

            Assert.Equal("2021-8-9 falls outside the expected month of 6", exception.Message);
        }

        [Fact]
        public void GetDate_ValidOccurrenceOfDay_ReturnsCorrectDate()
        {
            var holiday = new DayOfWeekPublicHoliday("Queen's Birthday", 6, 2, DayOfWeek.Monday);

            var holidayDate = holiday.GetDate(2021);

            Assert.Equal(new DateTime(2021, 6, 14), holidayDate);
        }

        [Fact]
        public void GetAdditionalDate_ByDefault_ReturnsNull()
        {
            var holiday = new DayOfWeekPublicHoliday("Queen's Birthday", 6, 10, DayOfWeek.Monday);

            var holidayDate = holiday.GetAdditionalDate(2021, null);

            Assert.Null(holidayDate);
        }

    }
}
