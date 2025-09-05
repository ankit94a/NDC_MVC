using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class CrsMbrRankService
    {
        public int PersonalServiceId { get; set; }
        public DateTime DOJoining { get; set; }
        public DateTime DOSeniority { get; set; }
        public string ServiceNo { get; set; }
        public string Service { get; set; }
        public string Branch { get; set; }

        public int RankId { get; set; }
        public virtual RankMaster Ranks { get; set; }
    }
}