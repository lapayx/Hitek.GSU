using Hitek.GSU.Logic.Interfaces;
using Hitek.GSU.Models;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Hitek.GSU.Logic.Service
{
    public class AccountService : IAccountService
    {
        AppSignInManager signInManager;

        long currentId;

        public  AccountService(AppSignInManager signInManager)
        {
            this.signInManager = signInManager;
            this.initial();
            
        }


        protected   void initial() {

            currentId = HttpContext.Current.User.Identity.GetUserId<long>();
           
        }

        public long GetCurrentUserId() {
            return currentId;
        }
        public ApplicationUser  GetCurrentUser()
        {
            return this.signInManager.UserManager.FindByIdAsync(currentId).Result;
        }
    }
}