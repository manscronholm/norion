using NorionTest.Application.Toll.Interfaces;

namespace NorionTest.Application.Toll;

public class TollFreeDateEvaluator : ITollFreeDateEvaluator
{
    private static readonly List<Func<DateTime, bool>> TollFreeRules =
    [
        date => date.DayOfWeek == DayOfWeek.Saturday,
        date => date.DayOfWeek == DayOfWeek.Sunday,
        date => date.Month == 7,
        date => date.Year == 2013 && IsHolidayOrDayBeforeHoliday(date)
    ];

    private static readonly List<DateTime> Holidays =
    [
        // January
        new(2013, 1, 1),
        // March
        new(2013, 3, 28),
        new(2013, 3, 29),
        // April
        new(2013, 4, 1),
        new(2013, 4, 30),
        // May
        new(2013, 5, 1),
        new(2013, 5, 8),
        new(2013, 5, 9),
        // June
        new(2013, 6, 5),
        new(2013, 6, 6),
        new(2013, 6, 21),
        // November
        new(2013, 11, 02),
        // December
        new(2013, 12, 24), 
        new(2013, 12, 25), 
        new(2013, 12, 26), 
        new(2013, 12, 31),
    ];
    
    public bool IsTollFreeDate(DateTime date)
    {
        return TollFreeRules.Any(rule => rule(date));
    }
    
    private static bool IsHolidayOrDayBeforeHoliday(DateTime date)
    {
        return Holidays.Contains(date) || Holidays.Contains(date.AddDays(-1));
    }
}