using Hitek.GSU.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database.Model
{
    public class Test
    {
        [Key] 
        public long Id { get; set; }

        [StringLength(200)]
        [Required] 
        public string Name { get; set; }

        public long? TestSubjectId { get; set; }

        [Required] 
        public long? AutorId { get; set; }


        public int CountQuestionForShow { get; set; }

        public bool IsHide { get; set; }

        public virtual TestSubject TestSubject { get; set; }

        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
        public virtual ICollection<TestHistory> TestHistories { get; set; }

        public virtual ICollection<WorkTest> WorkTests { get; set; }

    }

   
}