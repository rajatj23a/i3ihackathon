using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DecodingINS.Models
{
    public class CCDModel
    {
        [Required(ErrorMessage = "Error:Must Choose a Region")]
        public string region { get; set; }
        [Required(ErrorMessage = "Error:Must Choose a Country")]
        public string country { get; set; }
        [Required(ErrorMessage = "Error:Must Choose a State")]
        public string city_state { get; set; }
        [Required(ErrorMessage = "Error:Must Choose an Individual")]
        public string city_person { get; set; }

    }
}