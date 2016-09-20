using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Models
{
    public class TestInfo:Entity
    {
        public long TestSubjectId { get; set; }
        public string TestSubjectName { get; set; }
    }
}