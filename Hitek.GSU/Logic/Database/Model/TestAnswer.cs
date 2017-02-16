using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database.Model
{
    public class TestAnswer
    {
        [Key] 
        public long Id { get; set; }

   
        [Required] 
        public string Text { get; set; }

        public bool IsRight { get; set; }

        public long TestQuestionId { get; set; }

        public bool IsHide { get; set;}

        public virtual TestQuestion TestQuestion { get; set; } 

    }

   
}