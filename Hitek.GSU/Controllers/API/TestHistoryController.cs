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
    [Authorize]
    public class TestHistoryController : ApiController
    {

        readonly ITestHistoryService testservice;
        IAccountService accountService;

        public TestHistoryController(ITestHistoryService testservice,IAccountService accountService)
        {
            this.testservice = testservice;
            this.accountService = accountService;
        }

        // GET: api/TestHistory
        public IEnumerable<HistoryResult> Get()
        {
            return this.testservice.GetAllHistoryTestByUserId(accountService.GetCurrentUserId());
        }

        // GET: api/TestHistory/5
        public TestFull Get(long id)
        {
           var res = this.testservice.GetHistoryTestById(id);
            if (res == null || !res.IsCanShowResultAnswer)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            return res;
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
