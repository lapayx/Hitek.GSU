using Hitek.GSU.Logic.Interfaces;
using Hitek.GSU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hitek.GSU.Controllers

{
    [RoutePrefix("TestSubject")]
    [Authorize]
    public class TestSubjectController : Controller
    {
        readonly ITestSubjectService subjectService;

        public TestSubjectController(ITestSubjectService service)
        {
            this.subjectService = service;
        }

        [HttpGet]
        [Route]
        public ActionResult Get()
        {
            return Json( subjectService.GetAllTestSubjects(),JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(long id)
        {
            return Json(subjectService.GetTestSubjectById(id),JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [Route]
        public ActionResult Post(TestSubjectForAdd value)
        {
            TestSubject n = new TestSubject(){Name = value.Name,ParentId = (value.IsParent == false) ? value.ParentId : null };
            bool res = this.subjectService.AddTestSubject(n);
            return Json(new { status = res });
        }

        [HttpPut]
        [Route("{id}")]
        public JsonResult Put(long id, TestSubjectForAdd value)
        {
            TestSubject e = new TestSubject() { Name = value.Name, ParentId = (value.IsParent == false) ? value.ParentId : null };
            bool res = this.subjectService.EditTestSubject(id, e);
            return Json(new { status = res });

        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
            this.subjectService.DeleteTestSubjectById(id);

        }
    }
}