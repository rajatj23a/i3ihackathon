using DecodingFunction.Common.Models;
using DecodingFunction.FocusGroup.Models;
using DecodingFunction.RealtimePremium.Models;
using DecodingFunction.RealtimePremium.Services;
using DecodingFunction.Service;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace DecodingFunction.Common.Services
{
    internal class Executor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static IEnumerable<BaseTableEntity> Execute(HttpRequest req)
        {
            string type = req.Query["type"];
            
            if (!string.IsNullOrEmpty(type) && type.Trim().ToLowerInvariant().Equals("RealTimePremium".ToLowerInvariant()))
            {
                var realTimePremium = new RealTimePremiumData();
                var searchParam = new PremiumSearchParam()
                {
                    Area = req.Query["area"],
                    PartitionKey = req.Query["country"],
                    ResultTable = req.Query["resultTable"],
                    UniqueIdentifier = req.Query["uid"]
                };

                return realTimePremium.Process(searchParam);
            }
            else //   "FocusGroup"
            {
                var focusGroupData = new FocusGroupData();
                var searchParam = new FocusGroupSearchParam()
                {
                    Area = req.Query["area"],
                    PartitionKey = req.Query["country"],
                    ResultTable = req.Query["resultTable"]
                };

                return focusGroupData.Process(searchParam);
            }

        }
    }
}
