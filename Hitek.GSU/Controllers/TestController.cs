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
        readonly ITestRepository testRepository;

        public TestController(ITestRepository testRepository) {
            this.testRepository = testRepository;
        }

        // GET: Test
        public ActionResult Index()
        {
            var res = testRepository.Test.Select(x=>new { Id =x.Id, Name = x.Name}).ToList();
            return Json(res,JsonRequestBehavior.AllowGet);
        }
    }
}