using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hitek.GSU.Logic.Interfaces;
using Hitek.GSU.Models;

namespace Hitek.GSU.Logic.Service
{
    public class TestService: ITestService
    {
        readonly ITestRepository testRepository;
        public TestService(ITestRepository testRepositpry) {
            this.testRepository = testRepositpry;
        }

        public TestFull GetTestById(long id)
        {
            TestFull res = new TestFull(); 
            var t = testRepository.Test.FirstOrDefault(x => x.Id == id);

            if (t != null) {
                res = new TestFull
                {
                    Id = t.Id,
                    Name = t.Name,
                    Questions = new List<TestQuestion>()
                };
                foreach (var q in t.TestQuestions) {
                    TestQuestion qu = new TestQuestion { 
                        Id = q.Id,
                        Name = q.Name,
                        Text = q.Text,
                        Answers = new List<TestAnswer>() 
                    };
                                        
                    foreach (var a in q.TestAnswers) {
                        qu.Answers.Add(new TestAnswer
                        {
                            Id = a.Id,
                            Text = a.Text,
                            Name =""

                        });
                    }
                    res.Questions.Add(qu);
                }
            
            }
            return res;
        }

        public object CheckTest(Hitek.GSU.Models.Validation.Test.TestForCheack raw) {

            int right = 0;
            foreach (var q in raw.answers) {
                long ra = testRepository.TestAnswer.Where(x => x.TestQuestionId == q.questionId && x.IsRight == true).Select(x => x.Id).FirstOrDefault();
                if (ra == q.answerId)
                    right += 1;

            }
            float r = (float)right/raw.answers.Count ;

            Hitek.GSU.Logic.Database.TestHistory tt = new Hitek.GSU.Logic.Database.TestHistory(){
                Result = r,
                TestId = raw.idTest
            };

            testRepository.TestHistory.Add(tt);
            testRepository.SaveChanges();

            return new { id =tt.Id,res = r,total = raw.answers.Count, right=right };
        }

        public HistoryResult GetHistoryTestById(long id)
        {
            HistoryResult res = testRepository.TestHistory.Where(x => x.Id == id).Select(x => new HistoryResult() { Id = x.Id,Name = x.Test.Name, Result = x.Result }).FirstOrDefault();
            if (res == null)
                res = new HistoryResult();
            return res;
        }
        public ICollection<HistoryResult> GetAllHistoryTestByUserId(long id)
        {
            ICollection<HistoryResult> res= testRepository.TestHistory.Select(x => new HistoryResult() { Id= x.Id, Name = x.Test.Name, Result = x.Result }).ToList();
            return res;
        }

        public ICollection<TestInfo> GetAllTest()
        {
            return testRepository.Test.Select(x => new TestInfo() { Id = x.Id, Name = x.Name}).ToList();
        }

        public ICollection<TestInfo> GetTestBySubjectId(long subjectId)
        {
            IList<TestInfo> res = testRepository.Test.Where(x => x.TestSubjectId ==subjectId).Select(x => new TestInfo()
            {
                Id = x.Id,
                Name = x.Name
            }
            ).OrderBy(x => x.Name).ToList();
            return res;
        }
    }
}