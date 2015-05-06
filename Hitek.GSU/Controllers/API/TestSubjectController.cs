using Hitek.GSU.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hitek.GSU.Models;

namespace Hitek.GSU.Controllers.API
{
    [RoutePrefix("api/TestSubject")]
    public class TestSubjectController : ApiController
    {
        readonly ITestSubjectService subjectService;

        public TestSubjectController(ITestSubjectService service)
        {
            this.subjectService = service;
        }

        // GET: api/TestSubject
        public IEnumerable<TestSubject> Get()
        {
            return subjectService.GetAllTestSubjects();
        }

        // GET: api/TestSubject/5
        public TestSubject Get(long id)
        {
            return subjectService.GetTestSubjectById(id);
        }

        [Route("{id}/Test")]
        // GET: api/TestSubject/5/Test
        public IEnumerable<TestInfo> GetTestForTestSubject(long id)
        {
            return subjectService.GetListTestBySubjectId(id);
        }

        // POST: api/TestSubject
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/TestSubject/5
        public void Put(long id, TestSubject value)
        {
            var t = value;
        }

        // DELETE: api/TestSubject/5
        public void Delete(int id)
        {
            this.subjectService.DeleteTestSubjectById(id);

        }
    }
}
