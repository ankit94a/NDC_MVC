using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class Infotech: BaseEntity
    {
        [Key]
        public int ITId { get; set; }
        public bool LaptopFromDepartment { get; set; }
        public string DLaptopMake { get; set; }
        public string DModelNo { get; set; }
        public string DSlNo { get; set; }
        public bool DAdaptor { get; set; }
        public bool DBag { get; set; }
        public bool LaptopFromCollege { get; set; }
        public string CLaptopMake { get; set; }
        public string CModelNo { get; set; }
        public string CSlNo { get; set; }
        public bool CAdaptor { get; set; }
        public bool CBag { get; set; }

        #region File Path
        public string DeclarationFormDocPath { get; set; }
        public string InsuranceReceiptDocPath { get; set; }
        public string OutsidePermissionDocPath { get; set; }
        #endregion
    }
}