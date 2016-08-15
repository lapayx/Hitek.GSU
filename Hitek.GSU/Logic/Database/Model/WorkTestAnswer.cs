using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database.Model
{
    public class WorkTestAnswer
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long WorkQuestionId { get; set; }

        [Required]
        public long TestAnswerId { get; set; }

        public bool IsMark { get; set; }

        public DateTime? DateAnswered { get; set; }

        public virtual WorkTestQuestion WorkTestQuestion { get;set;}
        public virtual TestAnswer TestAnswer { get; set; }

    }
}