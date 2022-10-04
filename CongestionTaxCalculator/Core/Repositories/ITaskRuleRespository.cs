using CongestionTaxCalculator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.Repositories
{
    public interface ITaxRuleRespository
    {
        Task<IEnumerable<TaxRule>> GetCongestionTaxTime();
        Task<IEnumerable<FreeDate>> GetCongestionFreeDate();
    }
}

