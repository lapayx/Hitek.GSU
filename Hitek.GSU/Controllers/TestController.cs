using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hitek.GSU.Logic.Interfaces;

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
        public ActionResult Index(long id)
        {
            var res = testservice.GetTestById(id);
            return Json(res,JsonRequestBehavior.AllowGet);
        }
    }
}