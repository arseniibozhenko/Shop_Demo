using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Shop_Demo.Identity.Managers;
using Shop_Demo.Identity.Models;
using Shop_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Shop_Demo.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            LoginModel model = new LoginModel();
            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ApplicationUser emailUser = UserManager.FindByEmail(model.Email);
            if (emailUser != null)
            {
                ApplicationUser user = UserManager.Find(emailUser.UserName, model.Password);
                if (user != null)
                {
                    ClaimsIdentity identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = model.IsPersistent }, identity);

                    return Redirect(GetUrl(model.ReturnUrl));
                }
            }

            ModelState.AddModelError("", "Invalid email or password");
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
            //return RedirectToAction("Login", "Auth");
        }

        private string GetUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                return Url.Action("Index", "Home");

            return returnUrl;
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.PhoneNumber
            };

            //Создание пользователя
            IdentityResult result = UserManager.Create(user, model.Password);
            if (result.Succeeded)
            {
                IdentityResult result2 = UserManager.AddToRole(user.Id, "Customer");
                if (result2.Succeeded)
                {
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    GetErrorsFromResult(result2);
                }
            }
            else
            {
                GetErrorsFromResult(result);
            }

            return View(model);
        }

        private void GetErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}