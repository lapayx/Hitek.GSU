using Hitek.GSU.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Hitek.GSU.Controllers
{
    public class Account2Controller : Controller
    {
        AppUserManager userManager;
        IAuthenticationManager authenticationManager;
        AppSignInManager signInManager;

        public Account2Controller(AppUserManager userManager, AppSignInManager signInManager, IAuthenticationManager authenticationManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authenticationManager = authenticationManager;
        }

        

        

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false});
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return Json(new { success = true, rederictUrl = returnUrl });//RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return Json(new { success = false});//return View(model);
            }
        }


        [HttpPost]
        [AllowAnonymous]
        //   [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return Json(new { success = true });
                }
                //AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return Json(new { success = false});
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async  Task<ActionResult> AddMeAdmin()
        {
            var us = await this.userManager.FindByNameAsync(this.User.Identity.Name);
            var t = await this.userManager.AddToRoleAsync(us.Id, "Admin");
            if (t.Succeeded)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}