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
    [Authorize]
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

        [Authorize(Roles = "Admin, Teacher")]
        // POST: api/TestSubject
        public void Post(TestSubject value)
        {
            this.subjectService.AddTestSubject(value);
        }
        [Authorize(Roles = "Admin, Teacher")]
        // PUT: api/TestSubject/5
        public void Put(long id, TestSubjectForAdd value)
        {
            TestSubject e = new TestSubject() { Name = value.Name, ParentId = (value.IsParent == false) ? value.ParentId : null };
            bool res = this.subjectService.EditTestSubject(id, e);

            if (!res)
                NotFound(); 

        }

        [Authorize(Roles = "Admin, Teacher")]
        // DELETE: api/TestSubject/5
        public void Delete(int id)
        {
            this.subjectService.DeleteTestSubjectById(id);

        }
    }
}
