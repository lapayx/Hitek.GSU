using Hitek.GSU.Logic.Interfaces;
using Hitek.GSU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace Hitek.GSU.Controllers.API
{
    public class TestHistoryController : ApiController
    {

        readonly ITestService testservice;
        IAccountService accountService;

        public TestHistoryController(ITestService testservice,IAccountService accountService)
        {
            this.testservice = testservice;
            this.accountService = accountService;
        }

        // GET: api/TestHistory
        public IEnumerable<HistoryResult> Get()
        {
            User.Identity.GetUserId();
            return this.testservice.GetAllHistoryTestByUserId(accountService.GetCurrentUserId());
        }

        // GET: api/TestHistory/5
        public HistoryResult Get(long id)
        {
            return this.testservice.GetHistoryTestById(id);
        }


        // POST: api/TestHistory
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/TestHistory/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TestHistory/5
        public void Delete(int id)
        {
        }
    }
}
