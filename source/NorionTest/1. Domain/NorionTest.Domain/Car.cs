using NorionTest.Domain.Interfaces;

namespace NorionTest.Domain
{
    public class Car : IVehicle
    {
        public string GetVehicleType()
        {
            return "Car";
        }
    }
}