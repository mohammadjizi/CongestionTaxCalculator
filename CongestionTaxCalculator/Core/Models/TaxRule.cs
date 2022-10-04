using Newtonsoft.Json;

namespace CongestionTaxCalculator.Core.Models
{
    public class TaxRule
    {

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        public RulesEngine.Models.Rule [] Rules { get; set; }
        
    }
}

