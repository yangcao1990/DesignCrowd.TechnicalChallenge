using DesignCrowd.TechnicalChallenge.Models;

namespace DesignCrowd.TechnicalChallenge.Tests
{
    public class FixedDatePublicHolidayTests
    {
        [Fact]
        public void GetDate_WithoutAdjustment_InvalidDate_ThrowsArgumentException()
        {
            var holiday = new FixedDatePublicHoliday("Anzac Day", 4, 31, false);

            var exception = Assert.Throws<ArgumentException>(() => holiday.GetDate(2024));

            Assert.Equal("2024-4-31 is not a valid date in format of yyyy-M-d", exception.Message);
        }

        [Fact]
        public void GetDate_WithoutAdjustment_ValidDate_ReturnsCorrectDate()
        {
            var holiday = new FixedDatePublicHoliday("Anzac Day", 4, 25, false);

            var holidayDate = holiday.GetDate(2021);

            Assert.Equal(new DateTime(2021, 4, 25), holidayDate);
        }

        [Fact]
        public void GetAdditionalDate_WithoutAdjustment_InvalidDate_ThrowsArgumentException()
        {
            var holiday = new FixedDatePublicHoliday("Anzac Day", 4, 31, false);

            var exception = Assert.Throws<ArgumentException>(() => holiday.GetDate(2024));

            Assert.Equal("2024-4-31 is not a valid date in format of yyyy-M-d", exception.Message);
        }

        [Fact]
        public void GetAdditionalDate_WithoutAdjustment_ByDefault_ReturnsNull()
        {
            var holiday = new FixedDatePublicHoliday("Anzac Day", 4, 25, false);

            var holidayDate = holiday.GetAdditionalDate(2021, null);

            Assert.Null(holidayDate);
        }

        [Fact]
        public void GetDate_WithAdjustment_InvalidDate_ThrowsArgumentException()
        {
            var holiday = new FixedDatePublicHoliday("New Year's Day", 4, 31, true);

            var exception = Assert.Throws<ArgumentException>(() => holiday.GetDate(2024));

            Assert.Equal("2024-4-31 is not a valid date in format of yyyy-M-d", exception.Message);
        }

        [Fact]
        public void GetDate_WithAdjustment_ValidDate_ReturnsCorrectDate()
        {
            var holiday = new FixedDatePublicHoliday("New Year's Day", 1, 1, true);

            var holidayDate = holiday.GetDate(2021);

            Assert.Equal(new DateTime(2021, 1, 1), holidayDate);
        }

        [Fact]
        public void GetAdditionalDate_WithAdjustment_InvalidDate_ThrowsArgumentException()
        {
            var holiday = new FixedDatePublicHoliday("New Year's Day", 4, 31, true);

            var exception = Assert.Throws<ArgumentException>(() => holiday.GetDate(2024));

            Assert.Equal("2024-4-31 is not a valid date in format of yyyy-M-d", exception.Message);
        }

        [Fact]
        public void GetAdditionalDate_WithAdjustment_ValidDate_ReturnsCorrectDate()
        {
            var holiday = new FixedDatePublicHoliday("New Year's Day", 1, 1, true);

            var holidayDate1 = holiday.GetAdditionalDate(2021, null);
            var holidayDate2 = holiday.GetAdditionalDate(2022, null);

            Assert.Null(holidayDate1);
            Assert.Equal(new DateTime(2022, 1, 3), holidayDate2);
        }

        [Fact]
        public void ChristmasDay_GetAdditionalDate_WithOtherPublicHolidays_ReturnsCorrectDate()
        {
            var christmasDay = new FixedDatePublicHoliday("Christmas Day", 12, 25, true);
            var boxingDay = new FixedDatePublicHoliday("Boxing Day", 12, 26, true);

            var holidays = new List<PublicHoliday>() { christmasDay, boxingDay };

            var christmasDay1 = christmasDay.GetAdditionalDate(2021, holidays);
            var christmasDay2 = christmasDay.GetAdditionalDate(2022, holidays);
            var christmasDay3 = christmasDay.GetAdditionalDate(2023, holidays);

            Assert.Equal(new DateTime(2021, 12, 27), christmasDay1);
            Assert.Equal(new DateTime(2022, 12, 27), christmasDay2);
            Assert.Null(christmasDay3);
        }

        [Fact]
        public void BoxingDay_GetAdditionalDate_WithOtherPublicHolidays_ReturnsCorrectDate()
        {
            var christmasDay = new FixedDatePublicHoliday("Christmas", 12, 25, true);
            var boxingDay = new FixedDatePublicHoliday("Boxing Day", 12, 26, true);

            var holidays = new List<PublicHoliday>() { christmasDay, boxingDay };

            var boxingDay1 = boxingDay.GetAdditionalDate(2021, holidays);
            var boxingDay2 = boxingDay.GetAdditionalDate(2022, holidays);
            var boxingDay3 = boxingDay.GetAdditionalDate(2023, holidays);

            Assert.Equal(new DateTime(2021, 12, 28), boxingDay1);
            Assert.Null(boxingDay2);
            Assert.Null(boxingDay3);
        }

    }
}
