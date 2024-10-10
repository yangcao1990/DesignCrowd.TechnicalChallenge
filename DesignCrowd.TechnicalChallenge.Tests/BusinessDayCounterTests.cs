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

    }
}