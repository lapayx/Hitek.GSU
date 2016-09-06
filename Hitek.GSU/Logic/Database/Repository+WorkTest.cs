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

        public IDbSet<WorkTest> WorkTest
        {
            get { return this.entity.WorkTest; }
        }

        public IDbSet<WorkTestAnswer> WorkTestAnswer
        {
            get { return this.entity.WorkTestAnswer; }
        }

        public IDbSet<WorkTestQuestion> WorkTestQuestion
        {
            get { return this.entity.WorkTestQuestion; }
        }
        
    }
}