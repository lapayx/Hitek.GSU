using Hitek.GSU.Logic.Database.Model;
using Hitek.GSU.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hitek.GSU.Controllers.API
{
    public class TestQuestionController : ApiController
    {
        readonly ITestRepository testRep;
        IAccountService accountService;
        public TestQuestionController(ITestRepository workTestRep, IAccountService accountService)
        {

            this.testRep = workTestRep;
            this.accountService = accountService;

        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public TestQuestion Post(TestQuestion value)
        {
            testRep.TestQuestion.Attach(value);
            testRep.TestQuestion.Add(value);

            testRep.SaveChanges();
            return value;
        }

        // PUT api/<controller>/5
        public void Put(int id, TestQuestion value)
        {

            var quest = testRep
               .TestQuestion
               .Where(x =>
                   x.Id == id)

               .FirstOrDefault();
            if (quest == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            quest.IsHide = value.IsHide;
            quest.Name = value.Name;
            quest.Text = value.Text;
            testRep.SaveChanges();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            TestQuestion que = testRep.TestQuestion.Find(id);
            if (que == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            foreach (var a in que.TestAnswers)
            {
                testRep.TestAnswer.Remove(a);
            }
            testRep.SaveChanges();
            testRep.TestQuestion.Remove(que);
            testRep.SaveChanges();
        }
    }
}