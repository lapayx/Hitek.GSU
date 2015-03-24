using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Hitek.GSU.Logic.Interfaces;
using System.Web.Mvc;

namespace Hitek.GSU.Models
{
    public class MyUserStore : IUserLoginStore<MyAccount>, IUserClaimStore<MyAccount>,
        IUserRoleStore<MyAccount>, IUserPasswordStore<MyAccount>,
        IUserSecurityStampStore<MyAccount>, IUserStore<MyAccount>
    {
        readonly IAccountRepository rep;

        public MyUserStore(IAccountRepository t) {
            rep = t;//DependencyResolver.Current.GetService<IAccountRepository>();// GetInstance<IFoo>();
        }
        public Task AddLoginAsync(MyAccount user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<MyAccount> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(MyAccount user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(MyAccount user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(MyAccount user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(MyAccount user)
        {
            throw new NotImplementedException();
        }

        public Task<MyAccount> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<MyAccount> FindByNameAsync(string userName)
        {
            MyAccount res = new MyAccount();
            var t = rep.Account.Where(x => x.Name == userName).FirstOrDefault();
            if (t != null) {
                res.UserName = t.Name;
            }
            await Task.Delay(10);
            if(t!=null)
            return res;
            else
                return null;
            
        }

        public Task UpdateAsync(MyAccount user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public Task AddClaimAsync(MyAccount user, System.Security.Claims.Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(MyAccount user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(MyAccount user, System.Security.Claims.Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(MyAccount user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(MyAccount user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(MyAccount user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(MyAccount user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(MyAccount user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(MyAccount user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(MyAccount user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSecurityStampAsync(MyAccount user)
        {
            throw new NotImplementedException();
        }

        public Task SetSecurityStampAsync(MyAccount user, string stamp)
        {
            throw new NotImplementedException();
        }
    }
}