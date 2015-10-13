using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hitek.GSU.Logic.Interfaces;
using Hitek.GSU.Models;
using Database = Hitek.GSU.Logic.Database;

namespace Hitek.GSU.Logic.Service
{
    public class TestService : ITestService
    {
        readonly ITestRepository testRepository;
        IAccountService accountService;
        public TestService(ITestRepository testRepositpry, IAccountService accountService)
        {
            this.testRepository = testRepositpry;
            this.accountService = accountService;
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
                    Questions = new List<TestQuestion>()
                };
                foreach (var q in t.TestQuestions)
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

                    foreach (var a in q.TestAnswers)
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

            Hitek.GSU.Logic.Database.TestHistory tt = new Hitek.GSU.Logic.Database.TestHistory()
            {
                Result = r,
                TestId = raw.idTest,
                AccountId = accountService.GetCurrentUserId()
            };

            testRepository.TestHistory.Add(tt);
            testRepository.SaveChanges();

            return new { id = tt.Id, res = r, total = raw.answers.Count, right = right };
        }

        public HistoryResult GetHistoryTestById(long id)
        {
            HistoryResult res = testRepository.TestHistory.Where(x => x.Id == id).Select(x => new HistoryResult() { Id = x.Id, Name = x.Test.Name, Result = x.Result }).FirstOrDefault();
            if (res == null)
                res = new HistoryResult();
            return res;
        }
        public ICollection<HistoryResult> GetAllHistoryTestByUserId(long id)
        {
            ICollection<HistoryResult> res = testRepository.TestHistory.Where(x => x.AccountId == id).Select(x => new HistoryResult() { Id = x.Id, Name = x.Test.Name, Result = x.Result }).ToList();
            return res;
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
            Database.Test workTest;
            if (raw.Id != null)
            {
                workTest = this.testRepository.Test.Where(x => x.Id == raw.Id).FirstOrDefault();
            }
            else
            {
                workTest = new Database.Test();
            }

            if (workTest == null)
                return false;
            else
            {
                workTest.Name = raw.Title;
                workTest.TestSubjectId = raw.SubjectId;
                if (raw.Id == null)
                    this.testRepository.Test.Add(workTest);

                this.testRepository.SaveChanges();

            }
            Database.TestQuestion question;
            Database.TestAnswer answer;
            foreach (var q in raw.Questions)
            {
                if (q.Id != null)
                {
                    question = this.testRepository.TestQuestion.Where(x => x.Id == q.Id).FirstOrDefault();
                }
                else
                {
                    question = new Database.TestQuestion();
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
                            answer = new Database.TestAnswer();
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
                    .Select(x => new Models.Validation.Admin.Test.CreatingTest { Id = x.Id, Title = x.Name, SubjectId = (long)x.TestSubjectId })
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