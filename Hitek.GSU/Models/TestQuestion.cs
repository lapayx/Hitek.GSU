using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Models
{
    public class TestQuestion: Entity
    {
        public string Text { get; set; }
        public IList<TestAnswer> Answers { get; set; }
    }
}