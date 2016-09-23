using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Models
{
    public class TestFull: Entity
    {
        public DateTime? EndDate { get; set; }
        public int CountQuestion { get; set; }
        public bool IsCanShowResultAnswer { get; set; }
        public IList<TestQuestion> Questions { get; set; }
    }
}