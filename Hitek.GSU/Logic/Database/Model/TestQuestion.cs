using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database
{
    public class TestQuestion
    {
        [Key] 
        public long Id { get; set; }

        [StringLength(200)]
        [Required] 
        public string Name { get; set; }

        [Required] 
        public string Text { get; set; }

        [Required]
        public long TestId { get; set; }


        public Test Test { get; set; } 
        public virtual List<TestAnswer> TestAnswers { get; set; }
    }

   
}