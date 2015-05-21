using System;
using System.Collections.Generic;

namespace Hitek.GSU.Models.Validation.Admin.Test
{
    public class CreatingTest
    {
        public long? Id { get; set; }

        public long SubjectId { get; set; }

        public string Title { get; set; }

        public ICollection<CreatingTestQuestion> Questions { get; set; }
    }
}