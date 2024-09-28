using NorionTest.Application.Extensions;
using NorionTest.Application.Interfaces;
using NorionTest.Application.TollCalculators.Interfaces;
using NorionTest.Domain.Interfaces;

namespace NorionTest.Application.TollCalculators;

public class TollCalculator
{
    private readonly ITollFeeCalculator _tollFeeCalculator;

    public TollCalculator(ITollFeeCalculator tollFeeCalculator)
    {
        _tollFeeCalculator = tollFeeCalculator;
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

            long diffInMillies = date.Millisecond - intervalStart.Millisecond;
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
        if (date.IsTollFreeDate() || vehicle.IsTollFreeVehicle()) return 0;
        return _tollFeeCalculator.CalculateTollFee(date);
    }
}