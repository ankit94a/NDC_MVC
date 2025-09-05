using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class UserPwdManger
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        [StringLength(50)]
        public string Username { get; set; } = null;

        [Column("password")]
        [StringLength(200)]
        public string Password { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? ModifyDate { get; set; }
    }
}