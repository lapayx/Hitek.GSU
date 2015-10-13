using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database
{
    public class TestHistory
    {
        [Key] 
        public long Id { get; set; }

        [Required]
        public float Result { get; set; } 
        
        [Required]
        public long TestId { get; set; }

        [Required]
        public long AccountId { get; set; }

        public virtual Test Test { get; set; }
        
    }

   
}