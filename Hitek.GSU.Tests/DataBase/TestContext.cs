using Hitek.GSU.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitek.GSU.Tests
{
    public partial class TestContextq : ISavingRepository
    {
        public TestContextq()
        {
            this.Tests = new TestDbSet<Hitek.GSU.Logic.Database.Model.Test>();
            this.TestQuestions = new TestDbSet<Hitek.GSU.Logic.Database.Model.TestQuestion>();
            this.TestAnswers = new TestDbSet<Hitek.GSU.Logic.Database.Model.TestAnswer>();
          //  this.Posts = new TestDbSet<Post>();
        }

        public DbSet<Hitek.GSU.Logic.Database.Model.Test> Tests { get; set; }
        public DbSet<Hitek.GSU.Logic.Database.Model.TestQuestion> TestQuestions { get; set; }
        public DbSet<Hitek.GSU.Logic.Database.Model.TestAnswer> TestAnswers { get; set; }

       // public DbSet<Post> Posts { get; set; }
        public int SaveChangesCount { get; private set; }
        public int SaveChanges()
        {
            this.SaveChangesCount++;
            return 1;
        }
    }
}
