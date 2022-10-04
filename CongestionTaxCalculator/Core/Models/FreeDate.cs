using Newtonsoft.Json;

namespace CongestionTaxCalculator.Core.Models
{
    public class FreeDate
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "year")]
        public int Year { get; set; }

        public int Month { get; set; }

        public int[] Days { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

