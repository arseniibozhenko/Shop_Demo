using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Demo.Identity.Models
{
    public class ApplicationRole : IdentityRole<int, CustomUserRole>
    {
        public ApplicationRole()
        {

        }

        public ApplicationRole(string name)
        {
            Name = name;
        }
    }
}
