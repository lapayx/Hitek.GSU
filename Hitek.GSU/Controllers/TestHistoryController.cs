using Hitek.GSU.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hitek.GSU.Controllers
{
    [RoutePrefix("TestHistory")]
    [Authorize]
    public class TestHistoryController : Controller
    {
        readonly ITestService testservice;
        IAccountService accountService;

        public TestHistoryController(ITestService testservice, IAccountService accountService)
        {
            this.testservice = testservice;
            this.accountService = accountService;
        }
        [HttpGet]
        [Route]
        public JsonResult Get()
        {
            var userId = accountService.GetCurrentUserId();
            return Json(this.testservice.GetAllHistoryTestByUserId(userId).OrderByDescending(x => x.Id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("{id}")]
        public JsonResult Get(long id)
        {
            return Json(this.testservice.GetHistoryTestById(id), JsonRequestBehavior.AllowGet);
        }
    }
}