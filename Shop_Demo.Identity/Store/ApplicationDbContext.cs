using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shop_Demo.Identity.Models;
using Shop_Demo.Identity.Managers;
using System.Security.Claims;

namespace Shop_Demo.Identity.Store
{
    public class DbInitialize : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        private async Task SeedAsync(ApplicationDbContext context)
        {
            //Создание роли администратора
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                CustomRoleStore store = new CustomRoleStore(context);
                ApplicationRoleManager manager = new ApplicationRoleManager(store);
                ApplicationRole role = new ApplicationRole { Name = "Admin" };

                await manager.CreateAsync(role);
            }
            //Создание роли покупателя
            if (!context.Roles.Any(r => r.Name == "Customer"))
            {
                CustomRoleStore store = new CustomRoleStore(context);
                ApplicationRoleManager manager = new ApplicationRoleManager(store);
                ApplicationRole role = new ApplicationRole { Name = "Customer" };

                await manager.CreateAsync(role);
            }
            //Создание пользователя
            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                CustomUserStore store = new CustomUserStore(context);
                ApplicationUserManager manager = new ApplicationUserManager(store);
                ApplicationUser user = new ApplicationUser()
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = "Admin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    DateOfBirth = DateTime.Parse("01.01.2001"),
                    PhoneNumber = "0123456789",
                    PhoneNumberConfirmed = true
                };

                //Создание пользователя
                await manager.CreateAsync(user, "AdminAdmin");

                //Добавление роли пользователю
                await manager.AddToRoleAsync(user.Id, "Admin");

                await manager.AddClaimAsync(user.Id, new Claim(ClaimTypes.GivenName, "Admin"));
                await manager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Gender, "Man"));
                await manager.AddClaimAsync(user.Id, new Claim(ClaimTypes.DateOfBirth, "01.01.2001"));
            }
            //Создание пользователя
            if (!context.Users.Any(u => u.UserName == "User"))
            {
                CustomUserStore store = new CustomUserStore(context);
                ApplicationUserManager manager = new ApplicationUserManager(store);
                ApplicationUser user = new ApplicationUser()
                {
                    FirstName = "User",
                    LastName = "User",
                    UserName = "User",
                    Email = "user@gmail.com",
                    EmailConfirmed = true,
                    DateOfBirth = DateTime.Parse("01.01.2001"),
                    PhoneNumber = "0123456789",
                    PhoneNumberConfirmed = true
                };

                //Создание пользователя
                await manager.CreateAsync(user, "UserUser");

                //Добавление роли пользователю
                await manager.AddToRoleAsync(user.Id, "Customer");
            }
        }

        protected override void Seed(ApplicationDbContext context)
        {
            Task.Run(async () => { await SeedAsync(context); }).Wait();

            base.Seed(context);
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext() : base("ShopIdentityDb")
        {
            if (!Database.Exists())
            {
                Database.SetInitializer<ApplicationDbContext>(new DbInitialize());
            }
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
