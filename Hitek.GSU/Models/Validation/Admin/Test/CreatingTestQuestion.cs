using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Models.Validation.Admin.Test
{
    public class CreatingTestQuestion
    {
        public long? Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsRemoved { get; set; }

        public ICollection<CreatingTestAnswer> Answers { get; set; }
    }
}