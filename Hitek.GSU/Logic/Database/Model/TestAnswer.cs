using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database
{
    public class TestAnswer
    {
        [Key] 
        public long Id { get; set; }

        [StringLength(200)]
        [Required] 
        public string Text { get; set; }

        public string IsRight { get; set; }

        public long TestQuestionId { get; set; }


        public TestQuestion TestQuestion { get; set; } 

    }

   
}