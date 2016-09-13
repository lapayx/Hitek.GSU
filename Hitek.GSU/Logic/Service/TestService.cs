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

        public TestFull GetTestById(long id,  bool isNew)
        {
            long testId = id;
            if (isNew)
            {
                testId = generateTest(id);
            }

            TestFull res = new TestFull();
            var t = workTestRepository.WorkTest.FirstOrDefault(x => x.Id == testId);

            if (t != null)
            {
                res = new TestFull
                {
                    Id = t.Id,
                    Name = t.Name,
                    Questions = new List<TestQuestion>()
                };
                foreach (var q in t.WorkTestQuestions)
                {
                    TestQuestion qu = new TestQuestion
                    {
                        Id = q.Id,
                        Name = q.Name,
                        Text = q.Text,
                        IsSingleAnswer = q.WorkTestAnswers.Where(x=>x.IsRight).Count() == 1 ,
                        Answers = new List<TestAnswer>()
                        
                    };

                    foreach (var a in q.WorkTestAnswers)
                    {
                        qu.Answers.Add(new TestAnswer
                        {
                            Id = a.Id,
                            Text = a.Text,
                            IsAnswered = a.IsAnswered
                        });
                    }
                    res.Questions.Add(qu);
                }

            }
            return res;
        }

        private long generateTest(long testId) {

            DB.WorkTest test = null;
            var t = testRepository.Test.FirstOrDefault(x => x.Id == testId);

            if (t != null)
            {
                test = new DB.WorkTest()
                {
                    UserId = accountService.GetCurrentUserId(),
                    TestId = testId,
                    StartDate = DateTime.UtcNow,
                    Name = t.Name
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
            if(test != null)
            return test.Id;
            return 0;
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

        public void DeleteTestById(long id)
        {
            var t = this.testRepository.Test.Where(x => x.Id == id).FirstOrDefault();
            if (t != null)
            {
                t.IsHide = true;
                this.testRepository.SaveChanges();
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


        public object CreateOrEditTest(Models.Validation.Admin.Test.CreatingTest raw)
        {
            Database.Model.Test workTest;
            if (raw.Id != null)
            {
                workTest = this.testRepository.Test.Where(x => x.Id == raw.Id).FirstOrDefault();
            }
            else
            {
                workTest = new Database.Model.Test();
                workTest.AutorId = 0;
                workTest.CountQuestionForShow = 10;
            }

            if (workTest == null)
                return false;
            else
            {
                workTest.Name = raw.Title;
                workTest.CountQuestionForShow = raw.CountQuestion;
                workTest.TestSubjectId = raw.SubjectId;
                if (raw.Id == null)
                    this.testRepository.Test.Add(workTest);

                this.testRepository.SaveChanges();

            }
            Database.Model.TestQuestion question;
            Database.Model.TestAnswer answer;
            foreach (var q in raw.Questions)
            {
                if (q.Id != null)
                {
                    question = this.testRepository.TestQuestion.Where(x => x.Id == q.Id).FirstOrDefault();
                }
                else
                {
                    question = new Database.Model.TestQuestion();
                    this.testRepository.TestQuestion.Add(question);
                }

                if (question != null)
                {
                    if (q.IsRemoved == true)
                    {
                        question.IsHide = true;
                    }
                    question.Name = q.Title;
                    question.TestId = workTest.Id;
                    question.Text = q.Content;

                    testRepository.SaveChanges();

                    foreach (var a in q.Answers)
                    {
                        if (a.Id != null)
                        {
                            answer = testRepository.TestAnswer.Where(x => x.Id == a.Id).FirstOrDefault();
                        }
                        else
                        {
                            answer = new Database.Model.TestAnswer();
                            testRepository.TestAnswer.Add(answer);
                        }
                        if (answer != null)
                        {
                            if (a.IsRemoved)
                            {
                                answer.IsHide = true;
                            }
                            answer.IsRight = a.IsRight;
                            answer.Text = a.Content;
                            answer.TestQuestionId = question.Id;
                            testRepository.SaveChanges();
                        }
                    }
                }
            }
            return 1;

        }


        public Models.Validation.Admin.Test.CreatingTest GetTestForEditById(long id)
        {
            Models.Validation.Admin.Test.CreatingTest res;
            res = this.testRepository.Test
                    .Where(x => x.Id == id)
                    .Select(x => new Models.Validation.Admin.Test.CreatingTest { Id = x.Id, Title = x.Name, SubjectId = (long)x.TestSubjectId, CountQuestion = x.CountQuestionForShow })
                    .FirstOrDefault();
            if (res != null)
            {
                res.Questions = this.testRepository.TestQuestion.Where(x => x.TestId == res.Id && !x.IsHide).Select(x => new Models.Validation.Admin.Test.CreatingTestQuestion { Id = x.Id, Title = x.Name, Content = x.Text }).ToList();
                foreach (var q in res.Questions)
                {
                    q.Answers = this.testRepository.TestAnswer.Where(x => x.TestQuestionId == q.Id && !x.IsHide).Select(x => new Models.Validation.Admin.Test.CreatingTestAnswer { Id = x.Id, Content = x.Text, IsRight = x.IsRight }).ToList();
                }

            }
            return res;
        }
    }
}