using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database.Model
{
    public class TestSubject
    {
        [Key] 
        public long Id { get; set; }

        [StringLength(200)]
        [Required] 
        public string Name { get; set; }

        public long? ParentId { get; set; }

        public bool IsHide { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }

   
}