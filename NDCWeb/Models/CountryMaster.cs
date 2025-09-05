using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class CountryMaster
    {
        [Key]
        public int CountryId { get; set; }
        public string SortName { get; set; }
        public string CountryName { get; set; }
        public int PhoneCode { get; set; }
    }
}