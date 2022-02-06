namespace DecodingINS.Models
{
    public class RealTimePremiumData
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }

        public string FullName { get; set; }

        public string SumInsured { get; set; }

        public string HistoricalData { get; set; }

        public string RealTimePremium { get; set; }
    }
}