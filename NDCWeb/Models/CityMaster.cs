using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class CityMaster
    {
        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }

        public int StateId { get; set; }
        public virtual StateMaster States { get; set; }
    }
}