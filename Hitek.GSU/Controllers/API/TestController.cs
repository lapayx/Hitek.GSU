using Hitek.GSU.Logic.Interfaces;
using Hitek.GSU.Models;
using Hitek.GSU.Models.Validation.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hitek.GSU.Controllers.API
{
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {

        readonly ITestService testservice;

        public TestController(ITestService testservice)
        {
            this.testservice = testservice;
        }

        // GET: api/Test
        public IEnumerable<TestFull> Get()
        {
            return new List<TestFull>();
        }

        // GET: api/Test/5
        public TestFull Get(long id)
        {
            return testservice.GetTestById(id);
        }


        // POST: api/Test
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Test/5
        public void Delete(int id)
        {
        }

        [Route("Check")]
        public object PostCheck(TestForCheack mod)
        {
            return testservice.CheckTest(mod);
        }
    }
}
