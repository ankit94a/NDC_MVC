using System.ComponentModel.DataAnnotations;

namespace NDCWeb.Models
{
    public class TelecommRequirement : BaseEntity
    {
        [Key]
        public int TelecommReqId { get; set; }
        public string HouseNo { get; set; }
        public bool ReqInternet { get; set; }
        public string ResidentialComplex { get; set; }
        public string TypeOfConnection { get; set; }
        public string Comments { get; set; }
    }
}