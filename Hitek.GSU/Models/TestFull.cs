using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Models
{
    public class TestFull: Entity
    {
        public IList<TestQuestion> Questions { get; set; }
    }
}