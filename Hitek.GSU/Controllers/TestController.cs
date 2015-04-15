using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hitek.GSU.Logic.Interfaces;
using Hitek.GSU.Models.Validation.Test;

namespace Hitek.GSU.Controllers
{
    public class TestController : Controller
    {
        readonly ITestService testservice;

        public TestController(ITestService testservice)
        {
            this.testservice = testservice;
        }

        // GET: Test
        [HttpGet]
        public ActionResult Index(long id)
        {
            var res = testservice.GetTestById(id);
            return Json(res,JsonRequestBehavior.AllowGet);
        }



        [HttpPut]
        public ActionResult Index2(TestForCheack mod)
        {
            //var res = testservice.GetTestById(id);
            //return Json(res,JsonRequestBehavior.AllowGet);
            return Json( testservice.CheackTest(mod), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult TestResultById(long id)
        {
            return Json(testservice.GetHistoryTestById(id), JsonRequestBehavior.AllowGet);
        } 
        [HttpGet]
        public ActionResult AllResultByUserId(long id)
        {
            return Json(testservice.GetAllHistoryTestByUserId(id), JsonRequestBehavior.AllowGet);
        }

    }
}