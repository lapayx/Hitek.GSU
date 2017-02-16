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

        public AccountService(AppSignInManager signInManager)
        {
            this.signInManager = signInManager;
            this.initial();

        }


        protected void initial()
        {

            currentId = HttpContext.Current.User.Identity.GetUserId<long>();

        }

        public long GetCurrentUserId()
        {
            return currentId;
        }
        //public User  GetCurrentUser()
        // {
        //    return this.signInManager.UserManager.FindByIdAsync(currentId).Result;
        //}


        public IList<User> GetAllUsers()
        {
            return this.signInManager.UserManager.Users.ToList()
                .Select(x => new User { Id = x.Id, LastLoginDate = (x.LastLoginDate != null) ? ((DateTime)x.LastLoginDate).ToLocalTime() : x.LastLoginDate, RegistrationDate = x.RegistrationDate.ToLocalTime(), UserName = x.UserName })
                .ToList();
        }


        public User GetUserById(long id)
        {
            var x = this.signInManager.UserManager.FindByIdAsync(id).Result;
            return new User { Id = x.Id,
                LastLoginDate = (x.LastLoginDate!=null)  ?((DateTime)x.LastLoginDate).ToLocalTime() : x.LastLoginDate,
                RegistrationDate = x.RegistrationDate.ToLocalTime() ,
                UserName = x.UserName };

        }


        public bool AddRole(long userId, string role)
        {
            var res = this.signInManager.UserManager.AddToRole(userId, role);
            return res.Succeeded;
        }


        public bool RemoveRole(long userId, params string[] role)
        {
            var res = this.signInManager.UserManager.RemoveFromRoles(userId, role);
            return res.Succeeded;
        }


    }
}