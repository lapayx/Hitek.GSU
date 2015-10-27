using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Hitek.GSU.Models
{

    public class UserRoleLongPk : IdentityUserRole<long>
    {
    }

    public class UserClaimLongPk : IdentityUserClaim<long>
    {
    }

    public class UserLoginLongPk : IdentityUserLogin<long>
    {
    }

    public class RoleLongPk : IdentityRole<long, UserRoleLongPk>
    {
        public RoleLongPk() { }
        public RoleLongPk(string name) { Name = name; }
    }

    public class UserStoreLongPk : UserStore<ApplicationUser, RoleLongPk, long,
        UserLoginLongPk, UserRoleLongPk, UserClaimLongPk>
    {
        public UserStoreLongPk(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class RoleStoreLongPk : RoleStore<RoleLongPk, long, UserRoleLongPk>
    {
        public RoleStoreLongPk(ApplicationDbContext context)
            : base(context)
        {
        }
    }



    //change bellow classes
    public class ApplicationUser : IdentityUser<long, UserLoginLongPk, UserRoleLongPk, UserClaimLongPk>
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(AppUserManager manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this,
                               DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(AppUserManager manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}