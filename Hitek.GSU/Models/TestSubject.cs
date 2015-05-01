using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Models
{
    public class TestSubject:Entity
    {
        public ICollection<TestSubject> Childrens { get; set;}

        public int CountTest { get; set; }
    }
}