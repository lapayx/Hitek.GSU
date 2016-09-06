using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hitek.GSU.Logic.Interfaces;
using System.Data.Entity;
using Hitek.GSU.Logic.Database.Model;

namespace Hitek.GSU.Logic.Database
{
    public partial class Repository: IWorkTestRepository
    {

        public IDbSet<WorkTest> Test
        {
            get { return this.entity.WorkTest; }
        }

        public IDbSet<WorkTestAnswer> TestAnswer
        {
            get { return this.entity.; }
        }

        public IDbSet<WorkTestQuestion> TestQuestion
        {
            get { return this.entity.TestQuestion; }
        }
        
    }
}