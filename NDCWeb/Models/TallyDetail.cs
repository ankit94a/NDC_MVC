using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class TallyDetail : BaseEntity
    {
        [Key]
        public int TallyId { get; set; }
        public string RankAbbr { get; set; }
        public string PassportName { get; set; }
        public string TabName { get; set; }
        public string NickName { get; set; }
        public string CountryService { get; set; }
        public string NameORRank { get; set; }
        public string ResidentialAddress { get; set; }
        public string MobileNo { get; set; }
        public string TelephoneNo { get; set; }
        public string BrandModelNo { get; set; }
        public string Colour { get; set; }
        public string RegistrationNo { get; set; }
        public string DrivingLicenseNo { get; set; }
        public string NoOfVehicle { get; set; }

        public string RegistrationCertificatePath { get; set; }
        public string DrivingLicensePath { get; set; }
    }
}