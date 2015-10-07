using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Hitek.GSU.Models;
using Microsoft.Owin.Security.DataProtection;
using System.Web.Security;
using System.Web.Configuration;

namespace Hitek.GSU
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
    public class ACService : IIdentityValidator<ApplicationUser>
        {


        public Task<IdentityResult> ValidateAsync(ApplicationUser item)
            {
                throw new NotImplementedException();
            }
        }


    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class AppUserManager : UserManager<ApplicationUser, long> 
    {
        public AppUserManager(IUserStore<ApplicationUser, long> store)
        : base(store)
       {
           
            // Configure validation logic for usernames
           this.UserValidator = new UserValidator<ApplicationUser, long>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
              /*  RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,*/
            };
          //  this.PasswordHasher = new OldSystemPasswordHasher();    
            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            this.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser, long>
            {
                MessageFormat = "Your security code is {0}"
            });
            this.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser, long>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            this.EmailService = new EmailService();
            this.SmsService = new SmsService();

            var dataProtectionProvider = Startup.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
           
                IDataProtector dataProtector = dataProtectionProvider.Create("ASP.NET Identity");

                this.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, long>(dataProtector);
            }
        }
       /// <summary> 
       /// Use Custom approach to verify password 
       /// </summary> 
       public class OldSystemPasswordHasher : PasswordHasher
       {
           public override string HashPassword(string password)
           {
               return base.HashPassword(password);
           }

           public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
           {

               //Here we will place the code of password hashing that is there in our current solucion.This will take cleartext anad hash 
               //Just for demonstration purpose I always return true.     
               if (hashedPassword == providedPassword)
               {


                   return PasswordVerificationResult.SuccessRehashNeeded;
               }
               else
               {
                   return PasswordVerificationResult.Failed;
               }
           }
       }
    }

    // Configure the application sign-in manager which is used in this application.
    public class AppSignInManager : SignInManager<ApplicationUser, long>
    {
        public AppSignInManager(AppUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((AppUserManager)UserManager);
        }

        public override async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            if (false &&   Membership.Provider.ValidateUser(userName, password))
            {

                var user = UserManager.FindByName(userName);
                if (user == null)
                {
                    return SignInStatus.Failure;
                }

                await base.SignInAsync(user, isPersistent, shouldLockout);
                return SignInStatus.Success;
            }
            
            return await base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
            //return SignInStatus.Failure;
        }

        public static AppSignInManager Create(IdentityFactoryOptions<AppSignInManager> options, IOwinContext context)
        {
            return new AppSignInManager(context.GetUserManager<AppUserManager>(), context.Authentication);
        }
    }
}
