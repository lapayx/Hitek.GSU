using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hitek.GSU.Logic.Interfaces;
using System.Data.Entity;
namespace Hitek.GSU.Logic.Database
{
    public partial class Repository: ITestRepository
    {

        public IDbSet<Test> Test
        {
            get { return this.Test; }
        }

        public IDbSet<TestAnswer> TestAnswer
        {
            get { return this.TestAnswer; }
        }

        public IDbSet<TestQuestion> TestQuestion
        {
            get { return this.TestQuestion; }
        }
    }
}