using DecodingFunction.Common.Models;
using System;

namespace DecodingFunction.RealtimePremium.Models
{
    internal class PremiumSmartGadgetData : BaseTableEntity
    {
        public string BloodOxygen { get; set; }
        public string Sleep { get; set; }
        public string BpHigher { get; set; }
        public string BpLower { get; set; }
        public string HeartRate { get; set; }
        public string Smoking { get; set; }
        public string Tobacco { get; set; }
        public string Weight { get; set; }
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }

        public int Age
        {
            get
            {
                if (string.IsNullOrEmpty(DateOfBirth) || !DateTime.TryParse(DateOfBirth, out DateTime dob)) return 0;

                return DateTime.Now.Year - dob.Year;
            }
        }

    }
}
