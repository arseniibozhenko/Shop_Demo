using Shop_Demo.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Shop_Demo.DAL.Repositories
{
    public class GoodsRepository : GenericRepository<Good>, IRepository<Good>
    {
        public GoodsRepository(DbContext context) : base(context)
        {

        }
    }
}