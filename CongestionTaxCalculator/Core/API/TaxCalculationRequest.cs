namespace CongestionTaxCalculator.Core.API
{
    public class TaxCalculationRequest
    {
        public string VehicleType { get; set; }

        public string[] Dates { get; set; }
    }
}

