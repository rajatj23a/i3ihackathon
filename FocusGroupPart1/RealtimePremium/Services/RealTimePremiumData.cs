using DecodingFunction.Common.Models;
using DecodingFunction.Common.Services;
using DecodingFunction.RealtimePremium.Models;
using System.Collections.Generic;
using System.Linq;

namespace DecodingFunction.RealtimePremium.Services
{
    internal class RealTimePremiumData
    {
        public IEnumerable<PremiumResult> Process(BaseSearchParam searchParam)
        {
            var tableStorageClient = new TableStorageClient();
            var premiumResultList = new List<PremiumResult>();
            var premiumResult = new PremiumResult();
            var premiumSearchParam = (PremiumSearchParam)searchParam;

            var premiumSmartGadgetData = tableStorageClient
                .GetData<PremiumSmartGadgetData>("tblPremiumSmartGadgetData", "India")
                .FirstOrDefault(x => x.RowKey == premiumSearchParam.UniqueIdentifier);

            if (premiumSmartGadgetData == null) return premiumResultList;

            
            
            premiumResult.Id = 1;
            premiumResult.PartitionKey = premiumSmartGadgetData.PartitionKey;
            premiumResult.RowKey= System.Guid.NewGuid().ToString();
            premiumResult.UniqueIdentifier = premiumSmartGadgetData.RowKey; // unique identifier
            premiumResult.Gender = premiumSmartGadgetData.Gender;
            premiumResult.Area = searchParam.Area;
            premiumResult.Age =  premiumSmartGadgetData.Age.ToString();
            premiumResult.FullName = premiumSmartGadgetData.FullName;
            premiumResult.HistoricalData = "0";
            premiumResult.RealTimePremium = "$100";
            premiumResult.SumInsured = "$1,346.76";
            premiumResultList.Add(premiumResult);

            var premiumHistoricalData = tableStorageClient.GetData<PremiumHistoricalData>("tblPremiumHistoricalData", "India");

            foreach(var phd in premiumHistoricalData)
            {
                var ageRange = phd.Age?.Split("-");

                if (ageRange == null || ageRange.Length != 2 || string.IsNullOrEmpty(ageRange[0]) || string.IsNullOrEmpty(ageRange[1]))
                        continue;

                if (int.Parse(ageRange[0].Trim()) <= int.Parse(premiumResult.Age) 
                    && int.Parse(ageRange[1].Trim()) >= int.Parse(premiumResult.Age))
                {
                    premiumResult.HistoricalData = phd.PremiumAmount;
                    break;
                }
            }

            var premiumInsuranceData = tableStorageClient.GetData<PremiumInsuranceData>("tblPremiumInsuranceData", "India");

            var sleep = premiumInsuranceData.FirstOrDefault(x => x.Key.Trim().ToLowerInvariant() == "sleep");
            if (sleep != null && !string.IsNullOrEmpty(premiumSmartGadgetData.Sleep))
            {
                var sleepRange = sleep.Data.Split("-");
                if (sleepRange != null && sleepRange.Length == 2 && !string.IsNullOrEmpty(sleepRange[0]) || !string.IsNullOrEmpty(sleepRange[1]))
                {
                    if (int.Parse(sleepRange[0].Trim()) <= int.Parse(premiumSmartGadgetData.Sleep)
                    && int.Parse(sleepRange[1].Trim()) >= int.Parse(premiumSmartGadgetData.Sleep))
                    {
                        premiumResult.RealTimePremium = premiumResult.RealTimePremium+ 10; // value to be confimed
                    }
                }
            }
            premiumResult.HistoricalData = premiumResult.HistoricalData.Contains("$")? premiumResult.HistoricalData :"$"+ premiumResult.HistoricalData;
            premiumResult.RealTimePremium = premiumResult.RealTimePremium.Contains("$") ? premiumResult.RealTimePremium : "$" + premiumResult.RealTimePremium;

            tableStorageClient.SetData<PremiumResult>(searchParam.ResultTable, premiumResultList);

            return premiumResultList;
        }
    }
}
