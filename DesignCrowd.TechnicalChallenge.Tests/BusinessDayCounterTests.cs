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
        public void BusinessDaysBetweenTwoDates_FirstDateSameAsSecondDate_ReturnsZero()
        {
            var startDate = DateTime.Now;
            var count = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate, startDate, null);

            Assert.Equal(0, count);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_FirstDateAfterSecondDate_ReturnsZero()
        {
            var startDate = new DateTime(2013, 10, 7);
            var endDate = new DateTime(2013, 10, 5);
            var count = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate, endDate, null);

            Assert.Equal(0, count);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_FirstDateOneDayBeforeSecondDate_ReturnsZero()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(1);
            var count = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate, endDate, null);

            Assert.Equal(0, count);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_FirstDateSameWeekAsSecondDateWithoutPublicHolidays_ReturnsCorrectCount()
        {
            var startDate = new DateTime(2013, 10, 7);
            var endDate = new DateTime(2013, 10, 9);
            var count = _businessDayCounter.BusinessDaysBetweenTwoDates(startDate, endDate, null);

            Assert.Equal(1, count);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_FirstDateDifferentWeekFromSecondDateWithoutPublicHolidays_ReturnsCorrectCount()
        {
            var intervals = new List<(DateTime firstDate, DateTime endDate)>()
            {
                new(new DateTime(2013, 10, 5), new DateTime(2013, 10, 14)),
                new(new DateTime(2013, 10, 7), new DateTime(2014, 1, 1)),
            };

            var counts = intervals.Select(k => _businessDayCounter.BusinessDaysBetweenTwoDates(k.firstDate, k.endDate, null)).ToList();

            Assert.Equal(2, counts.Count);
            Assert.Equal(5, counts[0]);
            Assert.Equal(61, counts[1]);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDates_FirstDateBeforeSecondDateWithPublicHolidayDateOnly_ReturnsCorrectCount()
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
        public void BusinessDaysBetweenTwoDates_FirstDateBeforeSecondDateWithPublicHolidayDateTime_ReturnsCorrectCount()
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

    }
}