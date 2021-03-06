[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Shop_Demo.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Shop_Demo.App_Start.NinjectWebCommon), "Stop")]

namespace Shop_Demo.App_Start
{
    using System;
    using System.Data.Entity;
    using System.Web;
    using System.Web.Http;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Ninject.Web.WebApi;
    using Shop_Demo.BLL.DTO;
    using Shop_Demo.BLL.Services;
    using Shop_Demo.DAL.Context;
    using Shop_Demo.DAL.Repositories;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<ShopAdoEntities>();
            //Bind<IRepository<Good>>().To<GoodsRepository>().WithConstructorArgument("context", new ShopAdoEntities());

            kernel.Bind<IRepository<Good>>().To<GoodsRepository>();
            kernel.Bind<IRepository<Manufacturer>>().To<ManufacturersRepository>();
            kernel.Bind<IRepository<Category>>().To<CategoriesRepository>();
            kernel.Bind<IRepository<Photo>>().To<PhotosRepository>();
            kernel.Bind<IRepository<Sale>>().To<SalesRepository>();
            kernel.Bind<IRepository<SalePos>>().To<SalePosesRepository>();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IEntityService<GoodDTO>>().To<GoodsService>();
            kernel.Bind<IEntityService<ManufacturerDTO>>().To<ManufacturersService>();
            kernel.Bind<IEntityService<CategoryDTO>>().To<CategoriesService>();
            kernel.Bind<IEntityService<PhotoDTO>>().To<PhotosService>();
            kernel.Bind<IEntityService<SaleDTO>>().To<SalesService>();
            kernel.Bind<IEntityService<SalePosDTO>>().To<SalePosesService>();
        }
    }
}