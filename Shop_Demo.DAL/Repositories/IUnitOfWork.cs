using Shop_Demo.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Demo.DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Good> goodsRepository { get; }
        IRepository<Manufacturer> manufacturersRepository { get; }
        IRepository<Category> categoriesRepository { get; }
        IRepository<Photo> photosRepository { get; }
        IRepository<Sale> salesRepository { get; }
        IRepository<SalePos> salePosesRepository { get; }
        void Save();
    }
}
