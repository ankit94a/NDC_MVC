using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class TrainingProgrammes
    {
        public int TrgPgmeId { get; set; }
        public string TrgPgmeType { get; set; }
        public string TrgPgmeDesc { get; set; }
        public string UploadPath { get; set; }
        public string Comments { get; set; }
    }
}