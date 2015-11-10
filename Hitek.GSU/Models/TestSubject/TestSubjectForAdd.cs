using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Models
{
    public class TestSubjectForAdd
    {

        public string Name { get; set; }
        public bool IsParent { get; set; }
        public long? ParentId { get; set; }
    }
}