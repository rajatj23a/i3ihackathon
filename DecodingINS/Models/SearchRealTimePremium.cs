using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DecodingINS.Models
{
    public class SearchRealTimePremium
    {
        [Required(ErrorMessage = "Error:Must Choose a Region")]
        public string Region { get; set; }

        [Required(ErrorMessage = "Error:Must Choose an Area")]
        public string Area { get; set; }

        [Required(ErrorMessage = "Error:Must Choose a Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Error:Must Choose an Individual")]
        public string IndividualSelection { get; set; }
    }
}