using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hitek.GSU.Logic.Interfaces;
using Hitek.GSU.Models;
using Database = Hitek.GSU.Logic.Database;
using DB = Hitek.GSU.Logic.Database.Model;

namespace Hitek.GSU.Logic.Service
{
    public class TestService : ITestService
    {
        readonly ITestRepository testRepository;
        readonly IWorkTestRepository workTestRepository;
        IAccountService accountService;
        Random random;
        public TestService(ITestRepository testRepositpry, IAccountService accountService, IWorkTestRepository workTestRepository)
        {
            this.testRepository = testRepositpry;
            this.accountService = accountService;
            this.workTestRepository = workTestRepository;
            this.random = new Random((int)DateTime.Now.Ticks);
        }

        public TestInfo GetTestById(long id)
        {
            var test = testRepository.Test
                .Where(x => x.Id == id && !x.IsHide)

                .FirstOrDefault();
            if (test == null)
                return null;
            var res = new TestInfo()
            {
                Id = test.Id,
                Name = test.Name,
                IsHide = test.IsHide

            };
            if (test.TestSubject != null)
            {
                res.TestSubjectId = (long)test.TestSubjectId;
                res.TestSubjectName = test.TestSubject.Name;
            }
            return res;
        }



        public TestFull GetExistTestById(long id, bool withRightAnswer = false)
        {
            long testId = id;


            TestFull res = new TestFull();
            var t = workTestRepository.WorkTest.FirstOrDefault(x => x.Id == testId);

            if (t != null)
            {
                res = new TestFull
                {
                    Id = t.Id,
                    Name = t.Name,
                    Questions = new List<TestQuestion>(),
                    EndDate = t.EndDate,
                    IsCanShowResultAnswer = t.IsCanShowResultAnswer
                };
                foreach (var q in t.WorkTestQuestions)
                {
                    TestQuestion qu = new TestQuestion
                    {
                        Id = q.Id,
                        Name = q.Name,
                        Text = q.Text,
                        IsSingleAnswer = q.WorkTestAnswers.Where(x => x.IsRight).Count() == 1,
                        Answers = new List<TestAnswer>()

                    };

                    foreach (var a in q.WorkTestAnswers)
                    {
                        qu.Answers.Add(new TestAnswer
                        {
                            Id = a.Id,
                            Text = a.Text,
                            IsAnswered = a.IsAnswered,
                            IsRight = withRightAnswer ? a.IsRight : false

                        });
                    }
                    res.Questions.Add(qu);
                }

            }
            return res;
        }

        public long GenerateTest(long testId)
        {

            DB.WorkTest test = null;
            var t = testRepository.Test.FirstOrDefault(x => x.Id == testId);

            if (t != null)
            {
                test = new DB.WorkTest()
                {
                    UserId = accountService.GetCurrentUserId(),
                    TestId = testId,
                    StartDate = DateTime.UtcNow,
                    Name = t.Name,
                    IsCanShowResultAnswer = t.IsCanShowResultAnswer
                };

                workTestRepository.WorkTest.Add(test);
                workTestRepository.SaveChanges();

                foreach (var q in t.TestQuestions.Where(x => !x.IsHide).OrderBy(x => random.Next()).Take(t.CountQuestionForShow))
                {
                    DB.WorkTestQuestion qu = new DB.WorkTestQuestion
                    {
                        WorkTestId = test.Id,
                        Name = q.Name,
                        Text = q.Text,
                        TestQuestionId = q.Id,
                    };

                    workTestRepository.WorkTestQuestion.Add(qu);
                    workTestRepository.SaveChanges();

                    foreach (var a in q.TestAnswers.Where(x => !x.IsHide).OrderBy(x => random.Next()))
                    {
                        DB.WorkTestAnswer an = new DB.WorkTestAnswer
                        {
                            IsAnswered = false,
                            IsRight = a.IsRight,
                            Text = a.Text,
                            TestAnswerId = a.Id,
                            WorkTestQuestionId = qu.Id,
                        };
                        workTestRepository.WorkTestAnswer.Add(an);
                        workTestRepository.SaveChanges();


                    }
                }

            }
            if (test != null)
                return test.Id;
            return -1;
        }

        public object CheckTest(Hitek.GSU.Models.Validation.Test.TestForCheack raw)
        {
            long id = raw.idTest;



            float right = 0;
            int total = workTestRepository.WorkTestQuestion.Where(x => x.WorkTestId == id).Count();

            var t = workTestRepository.WorkTestAnswer
                .Where(x => x.WorkTestQuestion.WorkTestId == id)
                .GroupBy(x => x.WorkTestQuestionId)
                .Select(x => new { isRight = x.Count(y => y.IsRight), isAnswered = x.Count(y => y.IsAnswered == y.IsRight && y.IsRight) }).ToList();

            foreach (var x in t)
            {
                if (x.isRight>0)
                    right += (x.isAnswered < x.isRight) ? (float)x.isAnswered / x.isRight : (float)x.isRight / x.isAnswered;

            }

            var test = workTestRepository.WorkTest.Where(x => x.Id == id).FirstOrDefault();
            test.Result = right / total;
            test.EndDate = DateTime.UtcNow;

            testRepository.SaveChanges();

            return new object();
        }



        public ICollection<TestInfo> GetAllTest()
        {
            return testRepository.Test.Where(x => !x.IsHide).Select(x => new TestInfo() { Id = x.Id, Name = x.Name }).ToList();
        }

        public void DisableTestById(long id)
        {
            var t = this.testRepository.Test.Where(x => x.Id == id).FirstOrDefault();
            if (t != null)
            {
                t.IsHide = true;
                this.testRepository.SaveChanges();
            }
        }
        public void EnableTestById(long id)
        {
            var t = this.testRepository.Test.Where(x => x.Id == id).FirstOrDefault();
            if (t != null)
            {
                t.IsHide = false;
                this.testRepository.SaveChanges();
            }
        }

        public void DeleteTestById(long id)
        {
            var test = this.testRepository.Test.Where(x => x.Id == id).FirstOrDefault();
            if(test != null)
            {
                var question = this.testRepository.TestQuestion.Where(x => x.TestId  == test.Id).ToList();
                foreach(var q in question)
                {
                    var answer = testRepository.TestAnswer.Where(x => x.TestQuestionId == q.Id).ToList();
                    foreach (var a in answer)
                        testRepository.TestAnswer.Remove(a);
                    testRepository.SaveChanges();
                    testRepository.TestQuestion.Remove(q);
                    testRepository.SaveChanges();
                }
                testRepository.Test.Remove(test);
                testRepository.SaveChanges();

            }
          
            
        }

        public ICollection<TestInfo> GetTestBySubjectId(long subjectId)
        {
            IList<TestInfo> res = testRepository.Test.Where(x => x.TestSubjectId == subjectId && !x.IsHide).Select(x => new TestInfo()
            {
                Id = x.Id,
                Name = x.Name
            }
            ).OrderBy(x => x.Name).ToList();
            return res;
        }


        public Models.Validation.Admin.Test.CreatingTest CreateOrEditTest(Models.Validation.Admin.Test.CreatingTest raw)
        {
            Database.Model.Test workTest;
            if (raw.Id != null)
            {
                workTest = this.testRepository.Test.Where(x => x.Id == raw.Id).FirstOrDefault();
            }
            else
            {
                workTest = new Database.Model.Test();
                workTest.AutorId = accountService.GetCurrentUserId();
                workTest.CountQuestionForShow = 10;
            }

            if (workTest == null)
                return null;

            workTest.Name = raw.Title;
            workTest.CountQuestionForShow = raw.CountQuestion;
            workTest.TestSubjectId = raw.SubjectId;
            workTest.IsCanShowResultAnswer = raw.IsCanShowResultAnswer;
            if (raw.Id == null)
            {
                this.testRepository.Test.Attach(workTest);
                this.testRepository.Test.Add(workTest);
            }
            this.testRepository.SaveChanges();

            raw.Id = workTest.Id;


            return raw;

        }


        public Models.Validation.Admin.Test.CreatingTest GetTestForEditById(long id)
        {
            Models.Validation.Admin.Test.CreatingTest res;
            res = this.testRepository.Test
                    .Where(x => x.Id == id)
                    .Select(x => new Models.Validation.Admin.Test.CreatingTest
                    {
                        Id = x.Id,
                        Title = x.Name,
                        SubjectId = (long)x.TestSubjectId,
                        CountQuestion = x.CountQuestionForShow,
                        IsCanShowResultAnswer = x.IsCanShowResultAnswer
                    })
                    .FirstOrDefault();
            if (res != null)
            {
                res.Questions = this.testRepository.TestQuestion
                    .Where(x => x.TestId == res.Id && !x.IsHide)
                    .Select(x => new Models.Validation.Admin.Test.CreatingTestQuestion
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Text = x.Text,
                        TestId = x.TestId
                    }).ToList();
                foreach (var q in res.Questions)
                {
                    q.Answers = this.testRepository.TestAnswer
                        .Where(x => x.TestQuestionId == q.Id && !x.IsHide)
                        .Select(x => new Models.Validation.Admin.Test.CreatingTestAnswer
                        {
                            Id = x.Id,
                            Text = x.Text,
                            IsRight = x.IsRight,
                            TestQuestionId = x.TestQuestionId
                        }).ToList();
                }

            }
            return res;
        }


    }
}