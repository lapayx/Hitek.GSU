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
        readonly ITestService testService;

        public TestController(ITestService testservice)
        {
            this.testService = testservice;
        }


        [HttpGet]
        [Route]
        public ActionResult Index1()
        {
            return Json(testService.GetAllTest(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("{id}")]
        public JsonResult Index2(long id)
        {
            TestFull res = testService.GetTestById(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Subject/{subjectId}")]
        public ActionResult GetByTestSubject(long subjectId)
        {
            IEnumerable<TestInfo> res = testService.GetTestBySubjectId(subjectId);
            return Json(res, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [Route("Edit/{id}")]
        public ActionResult GetForEdit(long id)
        {
            CreatingTest res = testService.GetTestForEditById(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }



        [HttpDelete]
        [Route("{id}")]
        public void Delete(long id)
        {
            this.testService.DeleteTestById(id);
        }

        [HttpPost]
        [Route("Check")]
        public ActionResult PostCheck(TestForCheack mod)
        {
            return Json(testService.CheckTest(mod));
        }

        [HttpPost]
        [Route("Edit")]
        public ActionResult PostCreateTest(CreatingTest mod)
        {
            return Json(testService.CreateOrEditTest(mod));
        }

        [HttpPut]
        [Route("Edit")]
        public ActionResult PutChangeTest(CreatingTest mod)
        {
            return Json(testService.CreateOrEditTest(mod));
        }
    }
}
