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
            TestFull res = null;;
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

        public object CheackTest(Hitek.GSU.Models.Validation.Test.TestForCheack raw) {

            int right = 0;
            foreach (var q in raw.answers) {
                long ra = testRepository.TestAnswer.Where(x => x.TestQuestionId == q.questionId && x.IsRight == true).Select(x => x.Id).FirstOrDefault();
                if (ra == q.answerId)
                    right += 1;

            }
            float r = (float)right/raw.answers.Count ;

            Hitek.GSU.Logic.Database.TestHistory tt = new Hitek.GSU.Logic.Database.TestHistory(){
                Result = r,
                TestId = raw.id
            };

            testRepository.TestHistory.Add(tt);
            testRepository.SaveChanges();

            return new { id =tt.Id,res = r,total = raw.answers.Count, right=right };
        }
    }
}