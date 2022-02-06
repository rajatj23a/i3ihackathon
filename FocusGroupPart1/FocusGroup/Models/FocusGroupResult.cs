using DecodingFunction.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecodingFunction.FocusGroup.Models
{
    internal class FocusGroupResult : BaseTableEntity
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public string AgeGroup { get; set; }
        public string Gender { get; set; }
        public string Income { get; set; }
        public string Status { get; set; }
    }
}
