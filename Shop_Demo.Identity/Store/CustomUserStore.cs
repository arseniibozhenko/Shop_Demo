using Microsoft.AspNet.Identity.EntityFramework;
using Shop_Demo.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Demo.Identity.Store
{
    public class CustomUserStore : UserStore<ApplicationUser, ApplicationRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context) : base(context)
        {

        }
    }
}
