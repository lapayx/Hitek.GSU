using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Models
{
    public class TestAnswer: Entity
    {
        public string Text { get; set; }
        public bool IsRight { get; set; }
    }
}