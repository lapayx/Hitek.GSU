using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database.Model
{
    public class WorkTest
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long  TestId { get; set; }

        [Required]
        public long UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(200)]
        [Required]
        public string Name { get; set; }



        public  virtual Test Test { get; set; }
        public virtual ICollection<WorkTestQuestion> WorkTestQuestions { get; set; }
    }
}