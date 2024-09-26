using NorionTest.Domain.Interfaces;

namespace NorionTest.Application.Extensions;

public static class VehicleExtensions
{
    public static bool IsTollFreeVehicle(this IVehicle? vehicle)
    {
        if (vehicle == null) return false;
        
        var tollFreeVehicleTypes = new List<string>
        {
            TollFreeVehicles.Motorbike.ToString(),
            TollFreeVehicles.Tractor.ToString(),
            TollFreeVehicles.Emergency.ToString(),
            TollFreeVehicles.Diplomat.ToString(),
            TollFreeVehicles.Foreign.ToString(),
            TollFreeVehicles.Military.ToString()
        };

        return tollFreeVehicleTypes.Contains(vehicle.GetVehicleType());
    }
}