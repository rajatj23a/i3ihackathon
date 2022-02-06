using DecodingFunction.Common.Models;
using DecodingFunction.Common.Services;
using DecodingFunction.FocusGroup.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DecodingFunction.Service
{
    internal class FocusGroupData
    {
        private const int AgeMinLimit = 15;
        private const int AgeMaxLimit = 75;
        private const int HouseHoldIncomeMinLimit = 1000;
        
        public IEnumerable<FocusGroupResult> Process(BaseSearchParam searchParam)
        {
            var tableStorageClient = new TableStorageClient();

            //"127-54-830-6574"
            //"397-78-800-0234"
            var insuranceDetails = tableStorageClient.GetData<InsuranceDetails>("tblInsuranceDetailsPart1", searchParam.PartitionKey ?? "India");

            //"397-78-800-0234" -- exclude
            //"397-78-800-0212"
            //"127-54-830-6574" -- exclude
            //"127-54-830-6512"
            //"233-43-231-0945"
            //"145-54-123-5326"
            var companyDetails = tableStorageClient.GetData<CompanyDetails>("tblInsuranceCompaniesData", searchParam.PartitionKey ?? "India");

            // filter uninsured details
            var exceptDetails = companyDetails.Select(x => x.UniqueIdentifier).Except(insuranceDetails.Select(x => x.UniqueIdentifier)).ToList();

            //"127-54-830-6512" -- include
            //"127-54-830-6574"
            //"145-54-123-5326" -- include
            //"233-43-231-0945" -- include
            //"397-78-800-0212" -- include
            //"397-78-800-0234"
            var individualDetails = tableStorageClient.GetData<IndividualDetails>("tblFocusGroupIndividuals", searchParam.PartitionKey ?? "India");

            var filteredUnInsuredDetails = individualDetails.Where(x => exceptDetails.Contains(x.RowKey));

            var finalList = new List<FocusGroupResult>();
            var id = 1;
            foreach(var fid in filteredUnInsuredDetails)
            {
                if (!string.IsNullOrEmpty(fid.IsEarning) && fid.IsEarning.Trim().ToLower() == "yes" 
                    && fid.Age <= AgeMaxLimit && fid.Age >= AgeMinLimit 
                    && !string.IsNullOrEmpty(fid.FamilyIncome) &&  double.Parse(fid.FamilyIncome.Trim()) >= HouseHoldIncomeMinLimit )
                {
                    var ageGroup = string.Empty;

                    for (var age = 36; age <= AgeMaxLimit; age += 10)
                    {
                        if (fid.Age >=age && fid.Age <= age+9)
                        {
                            ageGroup = $"{age}-{age + 9} years";
                            break;
                        }
                    }

                    //finalList.Add(fid);
                    finalList.Add(new FocusGroupResult()
                    {
                        Id = id++,
                        Area = searchParam.Area,
                        AgeGroup = ageGroup,
                        Gender = fid.Gender,
                        Income = double.Parse(fid.FamilyIncome) > 1000 ? "> $10k" : "< $10k",
                        Status = "UnInsured",
                        PartitionKey = fid.PartitionKey,
                        RowKey = Guid.NewGuid().ToString(),
                    });
                    
                }
            }

            tableStorageClient.SetData<FocusGroupResult>(searchParam.ResultTable,finalList);

            return finalList;
        }
    }
}
