using NorionTest.Domain.Interfaces;

namespace NorionTest.Application.Extensions;

public static class VehicleExtensions
{
    public static bool IsTollFreeVehicle(this IVehicle? vehicle)
    {
        if (vehicle == null) return false;
        
        var vehicleType = vehicle.GetVehicleType();
        return vehicleType.Equals(TollFreeVehicles.Motorbike.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString());
    }
}