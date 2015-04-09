using Hitek.GSU.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitek.GSU.Tests
{
    public partial class TestContext : ITestRepository
    {

        public IDbSet<Logic.Database.Test> Test
        {
            get { return this.Tests; }
        }

        public IDbSet<Logic.Database.TestAnswer> TestAnswer
        {
            get { return this.TestAnswers; }
        }

        public IDbSet<Logic.Database.TestQuestion> TestQuestion
        {
            get { return this.TestQuestions; }
        }
    }
}
