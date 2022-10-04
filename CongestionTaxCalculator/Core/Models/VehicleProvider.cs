using Azure.Core;

namespace CongestionTaxCalculator.Core.Models
{
    public class VehicleProvider : IVehicleProvider
    {
        public IVehicle GetVehicle(string vehicleType)
        {
            switch (vehicleType.ToLower())
            {
                case "car": return new Car(vehicleType); 
                case "motorbike": return new Motorbike(vehicleType); 
                default: return new Vehicle(vehicleType); 
            }
        }
    }
}

