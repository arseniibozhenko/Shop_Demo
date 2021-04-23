using Shop_Demo.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Shop_Demo.DAL.Repositories
{
    public class PhotosRepository : GenericRepository<Photo>, IRepository<Photo>
    {
        public PhotosRepository(DbContext context) : base(context)
        {

        }
    }
}