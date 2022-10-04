namespace CongestionTaxCalculator.Core.Models
{
    public interface IVehicleProvider
    {
        public IVehicle GetVehicle(string vehicleType);
    }
}

