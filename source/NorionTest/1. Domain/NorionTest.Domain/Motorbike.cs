using NorionTest.Domain.Interfaces;

namespace NorionTest.Domain
{
    public class Motorbike : IVehicle
    {
        public string GetVehicleType()
        {
            return "Motorbike";
        }
    }
}
