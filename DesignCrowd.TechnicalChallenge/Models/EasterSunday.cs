namespace DesignCrowd.TechnicalChallenge.Models
{
    public class EasterSunday : PublicHoliday
    {
        public EasterSunday() : base("Easter Sunday")
        {
        }

        public override DateTime GetDate(int calendarYear)
        {
            var a = calendarYear % 19;
            var b = calendarYear / 100;
            var c = calendarYear % 100;
            var d = b / 4;
            var e = b % 4;
            var f = (b + 8) / 25;
            var g = (b - f + 1) / 3;
            var h = (19 * a + b - d - g + 15) % 30;
            var i = c / 4;
            var k = c % 4;
            var l = (32 + 2 * e + 2 * i - h - k) % 7;
            var m = (a + 11 * h + 22 * l) / 451;
            var month = (h + l - 7 * m + 114) / 31; // March = 3, April = 4
            var day = (h + l - 7 * m + 114) % 31 + 1;

            var dateString = $"{calendarYear}-{month}-{day}";

            return ParseDate(dateString);
        }

    }
}
