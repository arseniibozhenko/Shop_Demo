using Shop_Demo.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Shop_Demo.DAL.Repositories
{
    public class ManufacturersRepository : GenericRepository<Manufacturer>, IRepository<Manufacturer>
    {
        public ManufacturersRepository(DbContext context) : base(context)
        {

        }
    }
}