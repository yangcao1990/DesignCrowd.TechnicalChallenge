using DesignCrowd.TechnicalChallenge.Models;

namespace DesignCrowd.TechnicalChallenge.Tests
{
    public class BusinessDayCounterTests
    {
        private readonly BusinessDayCounter _businessDayCounter;

        public BusinessDayCounterTests()
        {
            _businessDayCounter = new BusinessDayCounter();
        }

        [Fact]
        public void WeekdaysBetweenTwoDates_FirstDateSameAsSecondDate_ReturnsZero()
        {
            var startDate = DateTime.Now;
            var count = _businessDayCounter.WeekdaysBetweenTwoDates(startDate, startDate);

            Assert.Equal(0, count);
        }

        [Fact]
        public void WeekdaysBetweenTwoDates_FirstDateAfterSecondDate_ReturnsZero()
        {
            var startDate = new DateTime(2013, 10, 7);
            var endDate = new DateTime(2013, 10, 5);
            var count = _businessDayCounter.WeekdaysBetweenTwoDates(startDate, endDate);

            Assert.Equal(0, count);
        }

        [Fact]
        public void WeekdaysBetweenTwoDates_FirstDateOneDayBeforeSecondDate_ReturnsZero()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(1);
            var count = _businessDayCounter.WeekdaysBetweenTwoDates(startDate, endDate);

            Assert.Equal(0, count);
        }

        [Fact]
        public void WeekdaysBetweenTwoDates_FirstDateSameWeekAsSecondDate_ReturnsCorrectCount()
        {
            var startDate = new DateTime(2013, 10, 7);
            var endDate = new DateTime(2013, 10, 9);
            var count = _businessDayCounter.WeekdaysBetweenTwoDates(startDate, endDate);

            Assert.Equal(1, count);
        }

        [Fact]
        public void WeekdaysBetweenTwoDates_FirstDateDifferentWeekFromSecondDate_ReturnsCorrectCount()
        {
            var intervals = new List<(DateTime firstDate, DateTime endDate)>()
            {
                new(new DateTime(2013, 10, 5), new DateTime(2013, 10, 14)),
                new(new DateTime(2013, 10, 7), new DateTime(2014, 1, 1)),
            };

            var counts = intervals.Select(k => _businessDayCounter.WeekdaysBetweenTwoDates(k.firstDate, k.endDate)).ToList();

            Assert.Equal(2, counts.Count);
            Assert.Equal(5, counts[0]);
            Assert.Equal(61, counts[1]);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_DateTimes_FirstDateSameAsSecondDate_ReturnsZero()
        {
            var startDate = DateTime.Now;
            var count = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate, startDate, new DateTime[] { });

            Assert.Equal(0, count);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_DateTimes_FirstDateAfterSecondDate_ReturnsZero()
        {
            var startDate = new DateTime(2013, 10, 7);
            var endDate = new DateTime(2013, 10, 5);
            var count = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate, endDate, new DateTime[] { });

            Assert.Equal(0, count);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_DateTimes_FirstDateOneDayBeforeSecondDate_ReturnsZero()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(1);
            var count = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate, endDate, new DateTime[] { });

            Assert.Equal(0, count);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_DateTimes_FirstDateSameWeekAsSecondDateWithoutPublicHolidays_ReturnsCorrectCount()
        {
            var startDate = new DateTime(2013, 10, 7);
            var endDate = new DateTime(2013, 10, 9);
            var count = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate, endDate, new DateTime[] { });

            Assert.Equal(1, count);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_DateTimes_FirstDateDifferentWeekFromSecondDateWithoutPublicHolidays_ReturnsCorrectCount()
        {
            var intervals = new List<(DateTime firstDate, DateTime endDate)>()
            {
                new(new DateTime(2013, 10, 5), new DateTime(2013, 10, 14)),
                new(new DateTime(2013, 10, 7), new DateTime(2014, 1, 1)),
            };

            var counts = intervals.Select(k => _businessDayCounter.BusinessDaysBetweenTwoDates(k.firstDate, k.endDate, new DateTime[] { })).ToList();

            Assert.Equal(2, counts.Count);
            Assert.Equal(5, counts[0]);
            Assert.Equal(61, counts[1]);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_DateTimes_FirstDateBeforeSecondDateWithPublicHolidayDateOnly_ReturnsCorrectCount()
        {
            var intervals = new List<(DateTime firstDate, DateTime endDate)>()
            {
                new(new DateTime(2013, 10, 7), new DateTime(2013, 10, 9)),
                new(new DateTime(2013, 12, 24), new DateTime(2013, 12, 27)),
                new(new DateTime(2013, 10, 7), new DateTime(2014, 1, 1)),
            };

            var publicHolidays = new List<DateTime>()
            {
                new(2013, 12, 25),
                new(2013, 12, 26),
                new(2014, 1, 1),
            };

            var counts = intervals.Select(k => _businessDayCounter.BusinessDaysBetweenTwoDates(k.firstDate, k.endDate, publicHolidays)).ToList();

            Assert.Equal(3, counts.Count);
            Assert.Equal(1, counts[0]);
            Assert.Equal(0, counts[1]);
            Assert.Equal(59, counts[2]);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_DateTimes_FirstDateBeforeSecondDateWithPublicHolidayDateTime_ReturnsCorrectCount()
        {
            var intervals = new List<(DateTime firstDate, DateTime endDate)>()
            {
                new(new DateTime(2013, 10, 7), new DateTime(2013, 10, 9)),
                new(new DateTime(2013, 12, 24), new DateTime(2013, 12, 27)),
                new(new DateTime(2013, 10, 7), new DateTime(2014, 1, 1)),
            };

            var publicHolidays = new List<DateTime>()
            {
                new(2013, 12, 25, 12, 0, 0),
                new(2013, 12, 26, 9, 0, 0),
                new(2014, 1, 1, 18, 0, 0),
            };

            var counts = intervals.Select(k => _businessDayCounter.BusinessDaysBetweenTwoDates(k.firstDate, k.endDate, publicHolidays)).ToList();

            Assert.Equal(3, counts.Count);
            Assert.Equal(1, counts[0]);
            Assert.Equal(0, counts[1]);
            Assert.Equal(59, counts[2]);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_PublicHolidays_FirstDateSameAsSecondDate_ReturnsZero()
        {
            var startDate = DateTime.Now;
            var count = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate, startDate, new List<PublicHoliday>());

            Assert.Equal(0, count);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_PublicHolidays_FirstDateAfterSecondDate_ReturnsZero()
        {
            var startDate = new DateTime(2013, 10, 7);
            var endDate = new DateTime(2013, 10, 5);
            var count = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate, endDate, new List<PublicHoliday>());

            Assert.Equal(0, count);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_PublicHolidays_FirstDateOneDayBeforeSecondDate_ReturnsZero()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(1);
            var count = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate, endDate, new List<PublicHoliday>());

            Assert.Equal(0, count);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_PublicHolidays_FirstDateSameWeekAsSecondDateWithoutPublicHolidays_ReturnsCorrectCount()
        {
            var startDate = new DateTime(2013, 10, 7);
            var endDate = new DateTime(2013, 10, 9);
            var count = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate, endDate, new List<PublicHoliday>());

            Assert.Equal(1, count);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_PublicHolidays_FirstDateDifferentWeekFromSecondDateWithoutPublicHolidays_ReturnsCorrectCount()
        {
            var intervals = new List<(DateTime firstDate, DateTime endDate)>()
            {
                new(new DateTime(2013, 10, 5), new DateTime(2013, 10, 14)),
                new(new DateTime(2013, 10, 7), new DateTime(2014, 1, 1)),
            };

            var counts = intervals.Select(k => _businessDayCounter.BusinessDaysBetweenTwoDates(k.firstDate, k.endDate, new List<PublicHoliday>())).ToList();

            Assert.Equal(2, counts.Count);
            Assert.Equal(5, counts[0]);
            Assert.Equal(61, counts[1]);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_PublicHolidays_FirstDateBeforeSecondDateWithPublicHolidays_ReturnsCorrectCount()
        {
            var intervals = new List<(DateTime firstDate, DateTime endDate)>()
            {
                new(new DateTime(2013, 10, 7), new DateTime(2013, 10, 9)),
                new(new DateTime(2013, 12, 24), new DateTime(2013, 12, 27)),
                new(new DateTime(2013, 10, 7), new DateTime(2014, 1, 1)),
            };

            var publicHolidays = new List<PublicHoliday>()
            {
                new FixedDatePublicHoliday("Christmas Day", 12, 25, true),
                new FixedDatePublicHoliday("Boxing Day", 12, 26, true),
                new FixedDatePublicHoliday("New Year's Day", 1, 1, true),
            };

            var counts = intervals.Select(k => _businessDayCounter.BusinessDaysBetweenTwoDates(k.firstDate, k.endDate, publicHolidays)).ToList();

            Assert.Equal(3, counts.Count);
            Assert.Equal(1, counts[0]);
            Assert.Equal(0, counts[1]);
            Assert.Equal(59, counts[2]);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_PublicHolidays_FirstDateBeforeSecondDateWithDifferentTypesPublicHolidays_ReturnsCorrectCount()
        {
            var intervals = new List<(DateTime firstDate, DateTime endDate)>()
            {
                new(new DateTime(2020, 12, 31), new DateTime(2022, 1, 1)),
                new(new DateTime(2021, 12, 31), new DateTime(2023, 1, 1)),
            };

            var publicHolidays = new List<PublicHoliday>()
            {
                new FixedDatePublicHoliday("New Year's Day", 1, 1, true),
                new FixedDatePublicHoliday("Australia Day", 1, 26, false),
                new RelativeDayOfWeekPublicHoliday("Good Friday", new EasterSunday(), true, DayOfWeek.Friday),
                new RelativeDayOfWeekPublicHoliday("Easter Saturday", new EasterSunday(), true, DayOfWeek.Saturday),
                new EasterSunday(),
                new RelativeDayOfWeekPublicHoliday("Easter Monday", new EasterSunday(), false, DayOfWeek.Monday),
                new FixedDatePublicHoliday("Anzac Day", 4, 25, false),
                new DayOfWeekPublicHoliday("Queen's Birthday", 6, 2, DayOfWeek.Monday),
                new DayOfWeekPublicHoliday("Labour Day", 10, 1, DayOfWeek.Monday),
                new FixedDatePublicHoliday("Christmas Day", 12, 25, true),
                new FixedDatePublicHoliday("Boxing Day", 12, 26, true),
            };

            var counts = intervals.Select(k => _businessDayCounter.BusinessDaysBetweenTwoDates(k.firstDate, k.endDate, publicHolidays)).ToList();

            Assert.Equal(2, counts.Count);
            Assert.Equal(253, counts[0]);
            Assert.Equal(251, counts[1]);
        }

    }
}