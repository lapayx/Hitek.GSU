using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hitek.GSU.Logic.Database
{
    public class Account
    {
        [StringLength(200)]
        public string Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }
    }

   
}