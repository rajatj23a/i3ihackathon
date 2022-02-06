using DecodingFunction.Common.Models;

namespace DecodingFunction.RealtimePremium.Models
{
    internal class PremiumInsuranceData : BaseTableEntity
    {
        public string Key { get; set; }
        public string Data { get; set; }
        public string MeasurementUnit { get; set; }
        public string Type { get; set; }
    }
}
