using Hitek.GSU.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hitek.GSU.Controllers.Admin
{
    [RoutePrefix("UserManager")]
    [Authorize(Roles="Admin")]
    public class UserController : Controller
    {
       

        IAccountService accountService;


        public UserController( IAccountService accountService)
        {
            this.accountService = accountService;
        }
        [HttpGet]
        [Route]
        public JsonResult Get()
        {
            var res = accountService.GetAllUsers();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("{id}")]
        public JsonResult Get(long id)
        {
            return Json(this.accountService.GetUserById(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("AddRole")]
        public JsonResult Get(long userId,string role)
        {
            bool resAddRole = this.accountService.AddRole(userId, role);
            return Json(new {success = resAddRole});
        }[
        HttpPost]
        [Route("RemoveAllRole")]
        public JsonResult RemoveAllRole(long userId)
        {
            bool resAddRole = this.accountService.RemoveRole(userId,"Admin","Teacher");
            return Json(new {success = resAddRole});
        }
    }
}