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
        public long WorkTestQuestionId { get; set; }

        [Required]
        public long TestAnswerId { get; set; }

       
        [Required]
        public string Text { get; set; }

        public bool IsRight { get; set; }

        public bool IsAnswered { get; set; }

        public DateTime? DateAnswered { get; set; }

        public virtual WorkTestQuestion WorkTestQuestion { get;set;}

    }
}