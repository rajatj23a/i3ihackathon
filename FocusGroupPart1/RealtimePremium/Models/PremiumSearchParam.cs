using DecodingFunction.Common.Models;
using Microsoft.Extensions.Primitives;

namespace DecodingFunction.RealtimePremium.Models
{
    internal class PremiumSearchParam : BaseSearchParam
    {
        public StringValues UniqueIdentifier { get; internal set; }
    }
}
