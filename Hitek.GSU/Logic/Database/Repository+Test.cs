using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hitek.GSU.Logic.Interfaces;
using System.Data.Entity;
using Hitek.GSU.Logic.Database.Model;

namespace Hitek.GSU.Logic.Database
{
    public partial class Repository: ITestRepository
    {

        public IDbSet<Test> Test
        {
            get { return this.entity.Test; }
        }

        public IDbSet<TestAnswer> TestAnswer
        {
            get { return this.entity.TestAnswer; }
        }

        public IDbSet<TestQuestion> TestQuestion
        {
            get { return this.entity.TestQuestion; }
        }

        public IDbSet<TestSubject> TestSubject
        {
            get { return this.entity.TestSubject; }
        }
    }
}