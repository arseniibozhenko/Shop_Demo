using Shop_Demo.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Demo.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext context;
        public IRepository<Good> goodsRepository { get; }
        public IRepository<Manufacturer> manufacturersRepository { get; }
        public IRepository<Category> categoriesRepository { get; }
        public IRepository<Photo> photosRepository { get; }
        public IRepository<Sale> salesRepository { get; }
        public IRepository<SalePos> salePosesRepository { get; }

        public UnitOfWork(DbContext context, IRepository<Good> goodsRepository, 
                                             IRepository<Manufacturer> manufacturersRepository,
                                             IRepository<Category> categoriesRepository, 
                                             IRepository<Photo> photosRepository,                          
                                             IRepository<Sale> salesRepository, 
                                             IRepository<SalePos> salePosesRepository)
        {
            this.context = context;
            this.goodsRepository = goodsRepository;
            this.manufacturersRepository = manufacturersRepository;
            this.categoriesRepository = categoriesRepository;
            this.photosRepository = photosRepository;
            this.salesRepository = salesRepository;
            this.salePosesRepository = salePosesRepository;
        }

        public void Save()
        {
            context.SaveChanges();
        }


        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
