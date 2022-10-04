using CongestionTaxCalculator.Core.Models;
using System.Collections.Generic;

namespace CongestionTaxCalculator.Core.API
{
    public class TaxCalculationResponse
    {
        public int TotalTax { get; set; }

        public IEnumerable<TaxInfo> TaxInfoList { get; set; }


        public TaxCalculationResponse(int _totalTax , IEnumerable<TaxInfo> _taxInfoList)
        {
            TotalTax = _totalTax;
            TaxInfoList = _taxInfoList;
        }
    }
}

