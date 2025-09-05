using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class SpouseLanguage : BaseEntity
    {
        [Key]
        public int LanguageId { get; set; }
        public string Language { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
        public bool Speak { get; set; }
        public string Qualification { get; set; }

        public int SpouseId { get; set; }
        public virtual CrsMbrSpouse Spouses { get; set; }
    }
}