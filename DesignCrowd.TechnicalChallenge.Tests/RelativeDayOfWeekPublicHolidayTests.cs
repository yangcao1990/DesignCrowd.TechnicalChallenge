using DesignCrowd.TechnicalChallenge.Models;

namespace DesignCrowd.TechnicalChallenge.Tests
{
    public class RelativeDayOfWeekPublicHolidayTests
    {
        [Fact]
        public void GetDate_InvalidCalendarYear_ThrowsArgumentException()
        {
            var easterSunday = new EasterSunday();
            var goodFriday = new RelativeDayOfWeekPublicHoliday("Good Friday", easterSunday, true, DayOfWeek.Friday);
            var easterSaturday = new RelativeDayOfWeekPublicHoliday("Easter Saturday", easterSunday, true, DayOfWeek.Saturday);
            var easterMonday = new RelativeDayOfWeekPublicHoliday("Easter Monday", easterSunday, false, DayOfWeek.Monday);

            var exception1 = Assert.Throws<ArgumentException>(() => goodFriday.GetDate(10));
            var exception2 = Assert.Throws<ArgumentException>(() => easterSaturday.GetDate(10));
            var exception3 = Assert.Throws<ArgumentException>(() => easterMonday.GetDate(10));

            Assert.Equal("10-4-18 is not a valid date in format of yyyy-M-d", exception1.Message);
            Assert.Equal("10-4-18 is not a valid date in format of yyyy-M-d", exception2.Message);
            Assert.Equal("10-4-18 is not a valid date in format of yyyy-M-d", exception3.Message);
        }

        [Fact]
        public void GetDate_ValidCalendarYear_ReturnsCorrectDate()
        {
            var easterSunday = new EasterSunday();
            var goodFriday = new RelativeDayOfWeekPublicHoliday("Good Friday", easterSunday, true, DayOfWeek.Friday);
            var easterSaturday = new RelativeDayOfWeekPublicHoliday("Easter Saturday", easterSunday, true, DayOfWeek.Saturday);
            var easterMonday = new RelativeDayOfWeekPublicHoliday("Easter Monday", easterSunday, false, DayOfWeek.Monday);

            Assert.Equal(new DateTime(2021, 4, 2), goodFriday.GetDate(2021));
            Assert.Equal(new DateTime(2022, 4, 15), goodFriday.GetDate(2022));
            Assert.Equal(new DateTime(2023, 4, 7), goodFriday.GetDate(2023));

            Assert.Equal(new DateTime(2021, 4, 3), easterSaturday.GetDate(2021));
            Assert.Equal(new DateTime(2022, 4, 16), easterSaturday.GetDate(2022));
            Assert.Equal(new DateTime(2023, 4, 8), easterSaturday.GetDate(2023));

            Assert.Equal(new DateTime(2021, 4, 5), easterMonday.GetDate(2021));
            Assert.Equal(new DateTime(2022, 4, 18), easterMonday.GetDate(2022));
            Assert.Equal(new DateTime(2023, 4, 10), easterMonday.GetDate(2023));
        }

        [Fact]
        public void GetAdditionalDate_ByDefault_ReturnsNull()
        {
            var easterSunday = new EasterSunday();
            var goodFriday = new RelativeDayOfWeekPublicHoliday("Good Friday", easterSunday, true, DayOfWeek.Friday);
            var easterSaturday = new RelativeDayOfWeekPublicHoliday("Easter Saturday", easterSunday, true, DayOfWeek.Saturday);
            var easterMonday = new RelativeDayOfWeekPublicHoliday("Easter Monday", easterSunday, false, DayOfWeek.Monday);

            Assert.Null(goodFriday.GetAdditionalDate(2021, null));
            Assert.Null(goodFriday.GetAdditionalDate(2022, null));
            Assert.Null(goodFriday.GetAdditionalDate(2023, null));

            Assert.Null(easterSaturday.GetAdditionalDate(2021, null));
            Assert.Null(easterSaturday.GetAdditionalDate(2022, null));
            Assert.Null(easterSaturday.GetAdditionalDate(2023, null));

            Assert.Null(easterMonday.GetAdditionalDate(2021, null));
            Assert.Null(easterMonday.GetAdditionalDate(2022, null));
            Assert.Null(easterMonday.GetAdditionalDate(2023, null));
        }

    }
}
