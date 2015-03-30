using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database
{
    public class Account
    {
        [Key] 
        public long Id { get; set; }

        [StringLength(200)]
        [Required] 
        public string Name { get; set; }

        [StringLength(200)]
        [Required]
        public string Password { get; set; }


        public virtual List<Role> Role { get; set; }
    }

   
}