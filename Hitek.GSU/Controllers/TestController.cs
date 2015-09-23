using Hitek.GSU.Logic.Interfaces;
using Hitek.GSU.Models;
using Hitek.GSU.Models.Validation.Admin.Test;
using Hitek.GSU.Models.Validation.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hitek.GSU.Controllers
{
    [RoutePrefix("Test")]
    [Authorize]
    public class TestController : Controller
    {

        readonly ITestService testservice;

        public TestController(ITestService testservice)
        {
            this.testservice = testservice;
        }

        // GET: api/Test
        public IEnumerable<TestInfo> Get()
        {
            return testservice.GetAllTest();
        }

        // GET: api/Test/Subject/{id}
        [Route("Subject/{subjectId}")]
        public IEnumerable<TestInfo> GetByTestSubject(long subjectId)
        {
            return testservice.GetTestBySubjectId(subjectId);
        }

        // GET: api/Test/5
        public TestFull Get(long id)
        {
            return testservice.GetTestById(id);
        }

        [Route("Edit/{id}")]
        // GET: api/Test/5
        public CreatingTest GetForEdit(long id)
        {
            return testservice.GetTestForEditById(id);
        }


        // POST: api/Test
        public void Post(string value)
        {
        }

        // PUT: api/Test/5
        public void Put(int id, string value)
        {
        }

        // DELETE: api/Test/5
        public void Delete(long id)
        {
            this.testservice.DeleteTestById(id);
        }

        [Route("Check")]
        public object PostCheck(TestForCheack mod)
        {
            return testservice.CheckTest(mod);
        }

        [Route("Edit")]
        public object PostCreateTest(CreatingTest mod)
        {
            return testservice.CreateOrEditTest(mod);
        }
        [Route("Edit")]
        public object PutChangeTest(CreatingTest mod)
        {
            return testservice.CreateOrEditTest(mod);
        }
    }
}
