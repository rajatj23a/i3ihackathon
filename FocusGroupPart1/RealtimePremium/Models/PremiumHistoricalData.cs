using DecodingFunction.Common.Models;

namespace DecodingFunction.RealtimePremium.Models
{
    internal class PremiumHistoricalData : BaseTableEntity
    {
        public string Age { get; set; }
        public string PremiumAmount { get; set; }
    }
}
