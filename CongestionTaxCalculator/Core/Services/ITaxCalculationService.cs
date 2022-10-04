using CongestionTaxCalculator.Core.Models;
using System;
using System.Collections.Generic;

namespace CongestionTaxCalculator.Core.Services
{
    public interface ITaxCalculationService
    {
        public KeyValuePair<int, IEnumerable<TaxInfo>> GetTax(IVehicle vehicle, IEnumerable<DateTime> dates);
    }
}

