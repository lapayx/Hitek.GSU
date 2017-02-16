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
    public enum ApplicationTypes
    {
        JavaScript = 0,
        NativeConfidential = 1
    }

    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public ApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public string AllowedOrigin { get; set; }
    }

    public class RefreshToken
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string ClientId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public string ProtectedTicket { get; set; }
    }



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
        public ApplicationUser() {
            this.RegistrationDate = DateTime.UtcNow; 
        }

        public DateTime RegistrationDate { get; set; }
        public DateTime? LastLoginDate { get; set; }

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