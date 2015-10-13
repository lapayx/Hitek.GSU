using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database
{
    public class Test
    {
        [Key] 
        public long Id { get; set; }

        [StringLength(200)]
        [Required] 
        public string Name { get; set; }

        public long? TestSubjectId { get; set; }
        


        public bool IsHide { get; set; }

        public virtual TestSubject TestSubject { get; set; } 
        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
        public virtual ICollection<TestHistory> TestHistories { get; set; }

    }

   
}