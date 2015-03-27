using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database
{
    public class Role
    {
        [Key] 
        public long Id { get; set; }
        [Required] 
        public long AccountId { get; set; } 
        public Account Account { get; set; } 
    }

   
}