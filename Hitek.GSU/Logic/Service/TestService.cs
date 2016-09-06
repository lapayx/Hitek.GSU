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

        public TestFull GetTestById(long id)
        {
            TestFull res = new TestFull();
            var t = testRepository.Test.FirstOrDefault(x => x.Id == id);

            if (t != null)
            {
                res = new TestFull
                {
                    Id = t.Id,
                    Name = t.Name,
                    CountQuestion = t.CountQuestionForShow,
                    Questions = new List<TestQuestion>()
                };
                foreach (var q in t.TestQuestions.OrderBy(x => random.Next()).Take(t.CountQuestionForShow))
                {
                    if (q.IsHide)
                        continue;
                    TestQuestion qu = new TestQuestion
                    {
                        Id = q.Id,
                        Name = q.Name,
                        Text = q.Text,
                        Answers = new List<TestAnswer>()
                    };

                    foreach (var a in q.TestAnswers.OrderBy(x => random.Next()))
                    {
                        if (a.IsHide)
                            continue;
                        qu.Answers.Add(new TestAnswer
                        {
                            Id = a.Id,
                            Text = a.Text,
                            Name = ""

                        });
                    }
                    res.Questions.Add(qu);
                }

            }
            return res;
        }

        private long generatetest(long testId) {

            DB.WorkTest test = null;
            var t = testRepository.Test.FirstOrDefault(x => x.Id == testId);

            if (t != null)
            {
                test = new DB.WorkTest()
                {
                    UserId = accountService.GetCurrentUserId(),
                    TestId = testId,
                    StartDate = DateTime.UtcNow,
                };

                workTestRepository.Test.Add(test);
                workTestRepository.SaveChanges();

                foreach (var q in t.TestQuestions.OrderBy(x => random.Next()).Take(t.CountQuestionForShow))
                {
                    if (q.IsHide)
                        continue;
                    DB.WorkTestQuestion qu = new DB.WorkTestQuestion
                    {
                        WorkTestId = test.Id,
                        
                        
                    };

                    foreach (var a in q.TestAnswers.OrderBy(x => random.Next()))
                    {
                        if (a.IsHide)
                            continue;
                        qu.Answers.Add(new TestAnswer
                        {
                            Id = a.Id,
                            Text = a.Text,
                            Name = ""

                        });
                    }
                    res.Questions.Add(qu);
                }

            }
            return res;

            return 0;
        }
        public object CheckTest(Hitek.GSU.Models.Validation.Test.TestForCheack raw)
        {

            int right = 0;
            foreach (var q in raw.answers)
            {
                long ra = testRepository.TestAnswer.Where(x => x.TestQuestionId == q.QuestionId && x.IsRight == true).Select(x => x.Id).FirstOrDefault();
                if (ra == q.AnswerId)
                    right += 1;

            }
            float r = (float)right / raw.answers.Count;

            Hitek.GSU.Logic.Database.Model.TestHistory tt = new Hitek.GSU.Logic.Database.Model.TestHistory()
            {
                Result = r,
                TestId = raw.idTest,
                AccountId = accountService.GetCurrentUserId()
            };

            testRepository.TestHistory.Add(tt);
            testRepository.SaveChanges();

            return new { id = tt.Id, res = r, total = raw.answers.Count, right = right };
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