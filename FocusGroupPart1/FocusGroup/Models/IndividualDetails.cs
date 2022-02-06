using DecodingFunction.Common.Models;
using System;

namespace DecodingFunction.FocusGroup.Models
{
    internal class IndividualDetails : BaseTableEntity
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string Region { get; set; }
        public string IsEarning { get; set; }
        public string HeadOfFamily { get; set; }
        public string Race { get; set; }
        public string FamilyIncome { get; set; }

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
