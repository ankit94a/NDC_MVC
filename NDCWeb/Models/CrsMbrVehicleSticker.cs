using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class CrsMbrVehicleSticker : BaseEntity
    {
        [Key]
        public int VehicleId { get; set; }
        public string BrandModelNo { get; set; }
        public string Colour { get; set; }
        public string RegistrationNo { get; set; }
        public string DrivingLicenseNo { get; set; }
        //public string NoOfVehicle { get; set; }
        public string RegistrationCertificatePath { get; set; }
        public string DrivingLicensePath { get; set; }
    }
}