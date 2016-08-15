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

        [StringLength(200)]
        [Required] 
        public string Text { get; set; }

        public bool IsRight { get; set; }

        public long TestQuestionId { get; set; }

        public bool IsHide { get; set;}

        [Required] 
        public long AccountId { get; set; }

        public virtual TestQuestion TestQuestion { get; set; } 

        public virtual ICollection<WorkTestAnswer> WorkTestAnswers { get; set; }

    }

   
}