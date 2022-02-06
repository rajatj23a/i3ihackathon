using DecodingFunction.Common.Models;

namespace DecodingFunction.FocusGroup.Models
{
    internal class InsuranceDetails : BaseTableEntity
    {
        public string CurrentInsuranceStatus { get; set; }
        public string UniqueIdentifier { get; set; }
    }
}
