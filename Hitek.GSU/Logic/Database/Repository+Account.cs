using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Hitek.GSU.Logic.Interfaces;

namespace Hitek.GSU.Logic.Database
{
    public partial class Repository : IAccountRepository
    {

        public IDbSet<Account> Account
        {
            get
            {
                return this.entity.Account;
            }
            
        }
    }
}