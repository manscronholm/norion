using NorionTest.Domain.Interfaces;

namespace NorionTest.Application.Toll.TollCalculators.Interfaces;

public interface ITollCalculator
{
    /**
    * Calculate the total toll fee for one day
    *
    * @param vehicle - the vehicle
    * @param passings - date and time of all passes on one day
    * @return - the total toll fee for that day
    */
    int GetTollFee(IVehicle vehicle, DateTime[] passings);
}