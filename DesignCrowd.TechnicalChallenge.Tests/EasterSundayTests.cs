using DesignCrowd.TechnicalChallenge.Models;

namespace DesignCrowd.TechnicalChallenge.Tests
{
    public class EasterSundayTests
    {
        [Fact]
        public void GetDate_InvalidCalendarYear_ThrowsArgumentException()
        {
            var holiday = new EasterSunday();

            var exception = Assert.Throws<ArgumentException>(() => holiday.GetDate(10));

            Assert.Equal("10-4-18 is not a valid date in format of yyyy-M-d", exception.Message);
        }

        [Fact]
        public void GetDate_ValidCalendarYear_ReturnsCorrectDate()
        {
            var holiday = new EasterSunday();

            Assert.Equal(new DateTime(2021, 4, 4), holiday.GetDate(2021));
            Assert.Equal(new DateTime(2022, 4, 17), holiday.GetDate(2022));
            Assert.Equal(new DateTime(2023, 4, 9), holiday.GetDate(2023));
        }

        [Fact]
        public void GetAdditionalDate_ByDefault_ReturnsNull()
        {
            var holiday = new EasterSunday();

            Assert.Null(holiday.GetAdditionalDate(2021, null));
            Assert.Null(holiday.GetAdditionalDate(2022, null));
            Assert.Null(holiday.GetAdditionalDate(2023, null));
        }

    }
}
