using Ninject.Modules;
using Shop_Demo.BLL.DTO;
using Shop_Demo.BLL.Services;
using Shop_Demo.DAL.Context;
using Shop_Demo.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Demo.NinjectDI
{
    public class ConfigureModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<ShopAdoEntities>();
            //Bind<IRepository<Good>>().To<GoodsRepository>().WithConstructorArgument("context", new ShopAdoEntities());

            Bind<IRepository<Good>>().To<GoodsRepository>();
            Bind<IRepository<Manufacturer>>().To<ManufacturersRepository>();
            Bind<IRepository<Category>>().To<CategoriesRepository>();
            Bind<IRepository<Photo>>().To<PhotosRepository>();
            Bind<IRepository<Sale>>().To<SalesRepository>();
            Bind<IRepository<SalePos>>().To<SalePosesRepository>();

            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IEntityService<GoodDTO>>().To<GoodsService>();
            Bind<IEntityService<ManufacturerDTO>>().To<ManufacturersService>();
            Bind<IEntityService<CategoryDTO>>().To<CategoriesService>();
            Bind<IEntityService<PhotoDTO>>().To<PhotosService>();
            Bind<IEntityService<SaleDTO>>().To<SalesService>();
            Bind<IEntityService<SalePosDTO>>().To<SalePosesService>();
        }
    }
}
