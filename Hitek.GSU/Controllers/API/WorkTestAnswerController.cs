using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hitek.GSU.Logic.Interfaces;
using Hitek.GSU.Models;

namespace Hitek.GSU.Controllers.API
{
    [RoutePrefix("api/WorkTestAnswer")]
    [Authorize]
    public class WorkTestAnswerController : ApiController
    {
        readonly IWorkTestRepository workTestRep;
        IAccountService accountService;
        public WorkTestAnswerController(IWorkTestRepository workTestRep, IAccountService accountService) {

            this.workTestRep = workTestRep;
            this.accountService = accountService;

        }
       
        /*// GET: api/WorkTestAnswer
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/WorkTestAnswer/5
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/WorkTestAnswer
        public void Post([FromBody]string value)
        {
        }
*/
        // PUT: api/WorkTestAnswer/5
        public HttpResponseMessage Put(int id, TestAnswer value)
        {
            var answer = workTestRep
                .WorkTestAnswer
                .Where(x => 
                    x.Id == id 
                    && x.WorkTestQuestion.WorkTest.EndDate == null 
                    && x.WorkTestQuestion.WorkTest.UserId == accountService.GetCurrentUserId() 
                    )
                .FirstOrDefault();
            if (answer != null)
            {
                answer.IsAnswered = value.IsAnswered;
                answer.DateAnswered = DateTime.UtcNow;
                workTestRep.SaveChanges();
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // DELETE: api/WorkTestAnswer/5
        public void Delete(int id)
        {
        }
    }
}
