namespace CongestionTaxCalculator.Core.Models
{
    public class Vehicle: IVehicle
    {
        protected string Type;

        public Vehicle()
        {
        }

        public Vehicle(string _type)
        {
            this.Type = _type;
        }

        public string GetVehicleType()
        {
            return this.Type;
        }
    }
}

