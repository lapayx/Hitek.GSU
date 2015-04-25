using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Models.Validation.Test
{
    public class TestForCheack

    {
        [Required]
        public long idTest { get; set; }
        public ICollection<Answer> answers { get; set; }
    }
}