using Hitek.GSU.Logic.Interfaces;
using Hitek.GSU.Models;
using Hitek.GSU.Models.Validation.Admin.Test;
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
    [Authorize]
    public class TestController : ApiController
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
        public TestInfo Get(long id)
        {
            var res = testservice.GetTestById(id);
            if (res == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return res;
        }

        [Route("Exist/{id}")]
        public TestFull GetExistTest(long id, bool exist = false)
        {

            TestFull res;

            res = testservice.GetExistTestById(id);
            if (res == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            if (res.EndDate != null)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            return res;
        }

        [Route("Generate/{id}")]
        public object GetGenerateTest(long id)
        {
            long res;

            res = testservice.GenerateTest(id);
            if (res < 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return new { id = res };
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
        [Authorize(Roles = "Admin, Teacher")]
        public void Delete(long id)
        {
            this.testservice.DeleteTestById(id);
        }

        [Route("Check")]
        public object PostCheck(TestForCheack mod)
        {
            return testservice.CheckTest(mod);
        }

        [Route("Edit/{id}")]
        [Authorize(Roles = "Admin, Teacher")]
        // GET: api/Test/5
        public CreatingTest GetForEdit(long id)
        {
            return testservice.GetTestForEditById(id);
        }

        [Route("Edit")]
        [Authorize(Roles = "Admin, Teacher")]
        public CreatingTest PostCreateTest(CreatingTest mod)
        {
            var res = testservice.CreateOrEditTest(mod);
            if (res == null) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return res;
        }
        [Route("Edit/{id}")]
        [Authorize(Roles = "Admin, Teacher")]
        public CreatingTest PutChangeTest(long id, CreatingTest mod)
        {
            var res = testservice.CreateOrEditTest(mod);
            if (res == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return res;
        }
    }
}
