using NorionTest.Application.Extensions;
using NorionTest.Application.Toll.Interfaces;
using NorionTest.Application.Toll.TollCalculators.Interfaces;
using NorionTest.Domain.Interfaces;

namespace NorionTest.Application.Toll.TollCalculators;

public class TollCalculator
{
    private readonly ITollFeeCalculator _tollFeeCalculator;
    private readonly ITollFreeDateEvaluator _tollFreeDateEvaluator;

    public TollCalculator(ITollFeeCalculator tollFeeCalculator, 
        ITollFreeDateEvaluator tollFreeDateEvaluator)
    {
        _tollFeeCalculator = tollFeeCalculator;
        _tollFreeDateEvaluator = tollFreeDateEvaluator;
    }

    /**
 * Calculate the total toll fee for one day
 *
 * @param vehicle - the vehicle
 * @param dates   - date and time of all passes on one day
 * @return - the total toll fee for that day
 */

    public int GetTollFee(IVehicle vehicle, DateTime[] dates)
    {
        var intervalStart = dates[0];
        var totalFee = 0;
        foreach (var date in dates)
        {
            var nextFee = GetTollFee(date, vehicle);
            var tempFee = GetTollFee(intervalStart, vehicle);

            var diffInMillies = (date - intervalStart).TotalMilliseconds;
            var minutes = diffInMillies/1000/60;

            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    private int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (_tollFreeDateEvaluator.IsTollFreeDate(date) || vehicle.IsTollFreeVehicle()) return 0;
        return _tollFeeCalculator.CalculateTollFee(date);
    }
}