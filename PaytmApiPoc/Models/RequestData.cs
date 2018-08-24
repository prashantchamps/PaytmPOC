using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaytmApiPoc.Models
{
    public class RequestData
    {
        [Required]
        public string mobileNumber { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string amount { get; set; }
    }
}