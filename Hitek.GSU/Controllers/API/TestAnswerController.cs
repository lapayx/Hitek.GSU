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
    [Authorize(Roles = "Admin, Teacher")]
    public class TestAnswerController : ApiController
    {
        readonly ITestRepository testRep;
        IAccountService accountService;
        public TestAnswerController(ITestRepository workTestRep, IAccountService accountService)
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
        public TestAnswer Post(TestAnswer value)
        {
            testRep.TestAnswer.Attach(value);
            testRep.TestAnswer.Add(value);

            testRep.SaveChanges();
            return value;
        }

        // PUT api/<controller>/5
        public void Put(int id, TestAnswer value)
        {
            var answer = testRep
               .TestAnswer
               .Where(x =>
                   x.Id == id)

               .FirstOrDefault();
            if (answer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            answer.IsHide = value.IsHide;
            answer.IsRight = value.IsRight;
            answer.Text = answer.Text;
            testRep.SaveChanges();

        }


        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            TestAnswer ans = testRep.TestAnswer.Find(id);
            if (ans == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            testRep.TestAnswer.Remove(ans);
            testRep.SaveChanges();
        }
    }
}