using NorionTest.Application.Extensions;
using NorionTest.Application.Toll.Interfaces;
using NorionTest.Application.Toll.TollCalculators.Interfaces;
using NorionTest.Domain.Interfaces;

namespace NorionTest.Application.Toll.TollCalculators;

public class TollCalculator(
    ITollFeeCalculator tollFeeCalculator,
    ITollFreeDateEvaluator tollFreeDateEvaluator) : ITollCalculator
{
    private const int MaximumDailyFee = 60;

    public int GetTollFee(IVehicle vehicle, DateTime[] passings)
    {
        if (passings.Length == 0) return 0;
        
        var initialPassing = passings.Min();
        var totalFee = 0;
        
        foreach (var passing in passings)
        {
            totalFee = UpdateTotalFee(passing, initialPassing, totalFee, vehicle);
        }
        
        return totalFee > MaximumDailyFee 
            ? MaximumDailyFee 
            : totalFee;
    }

    private int UpdateTotalFee(DateTime passing, DateTime initialPassing, int totalFee, IVehicle vehicle)
    {
        var currentFee = GetTollFee(passing, vehicle);
        var initialFee = GetTollFee(initialPassing, vehicle);
        
        if (VehiclePassedInTheLastHour(passing, initialPassing))
        {
            if (totalFee > 0) totalFee -= initialFee;
            if (currentFee >= initialFee) initialFee = currentFee;
            totalFee += initialFee;
            return totalFee;
        }

        totalFee += currentFee;
        return totalFee;
    }
    
    private int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (tollFreeDateEvaluator.IsTollFreeDate(date) || vehicle.IsTollFreeVehicle()) return 0;
        return tollFeeCalculator.CalculateTollFee(date);
    }

    private static bool VehiclePassedInTheLastHour(DateTime passing, DateTime initialPassing)
    {
        var diffInMilliseconds = (passing - initialPassing).TotalMilliseconds;
        var diffInMinutes = diffInMilliseconds/1000/60;
        return diffInMinutes <= 60;
    }
}