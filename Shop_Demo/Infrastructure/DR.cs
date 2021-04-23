using Ninject;
using Shop_Demo.DAL.Context;
using Shop_Demo.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Demo.Infrastructure
{
    public class DR : IDependencyResolver
    {
        IKernel kernel;

        public DR()
        {
            kernel = new StandardKernel();

            kernel.Bind<DbContext>().To<ShopAdoEntities>();
            //kernel.Bind<IRepository<Good>>().To<GoodsRepository>().WithConstructorArgument("context", new ShopAdoEntities());

            kernel.Bind<IRepository<Good>>().To<GoodsRepository>();
            kernel.Bind<IRepository<Manufacturer>>().To<ManufacturersRepository>();
            kernel.Bind<IRepository<Category>>().To<CategoriesRepository>();
            kernel.Bind<IRepository<Photo>>().To<PhotosRepository>();
            kernel.Bind<IRepository<Sale>>().To<SalesRepository>();
            kernel.Bind<IRepository<SalePos>>().To<SalePosesRepository>();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}