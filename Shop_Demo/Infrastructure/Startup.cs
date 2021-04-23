using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using Shop_Demo.Identity.Managers;
using Shop_Demo.Identity.Store;

[assembly: OwinStartup(typeof(Shop_Demo.Infrastructure.Startup))]

namespace Shop_Demo.Infrastructure
{
    public class Startup
    {
        //public static Func<UserManager<AppUser, int>> UserManagerFactory { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Auth/Login")
            });

            //UserManagerFactory = () =>
            //{
            //    var usermanager = new UserManager<ApplicationUser, int>(
            //        new CustomUserStore(new ApplicationDbContext()));
            //    // добавляем при необходимости валидацию!!!
            //    usermanager.UserValidator = new UserValidator<ApplicationUser, int>(usermanager)
            //    {
            //        AllowOnlyAlphanumericUserNames = false
            //    };

            //    return usermanager;
            //};
        }
    }
}
