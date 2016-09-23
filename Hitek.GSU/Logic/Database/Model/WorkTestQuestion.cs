using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database.Model
{
    public class WorkTestQuestion
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long WorkTestId { get; set; }

        [Required]
        public long TestQuestionId { get; set; }

        [StringLength(200)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Text { get; set; }

        public virtual WorkTest WorkTest { get; set; }
        public virtual TestQuestion TestQuestion { get; set; }
        public virtual ICollection<WorkTestAnswer> WorkTestAnswers { get; set; }
    }
}