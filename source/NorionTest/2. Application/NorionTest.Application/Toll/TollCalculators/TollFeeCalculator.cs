using NorionTest.Application.Toll.TollCalculators.Interfaces;
using NorionTest.Application.Toll.TollCalculators.Models;

namespace NorionTest.Application.Toll.TollCalculators;

public class TollFeeCalculator : ITollFeeCalculator
{
    private readonly ICollection<TollFeePeriod> _tollFeePeriods = new List<TollFeePeriod>
    {
        new(new TimeOnly(6, 0), new TimeOnly(6, 29, 59), 8),
        new(new TimeOnly(6, 30), new TimeOnly(6, 59, 59), 13),
        new(new TimeOnly(7, 0), new TimeOnly(7, 59, 59), 18),
        new(new TimeOnly(8, 0), new TimeOnly(8, 29, 59), 13),
        new(new TimeOnly(8, 30), new TimeOnly(14, 59, 59), 8),
        new(new TimeOnly(15, 0), new TimeOnly(15, 29, 59), 13),
        new(new TimeOnly(15, 30), new TimeOnly(16,59, 59), 18),
        new(new TimeOnly(17, 0), new TimeOnly(17, 59, 59), 13),
        new(new TimeOnly(18, 0), new TimeOnly(18, 29, 59), 8)
    };
    
    public int CalculateTollFee(DateTime date)
    {
        return _tollFeePeriods.FirstOrDefault(fee => fee.IsWithinPeriod(date))?.Fee ?? 0;
    }
}