using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Demo.BLL.Services
{
    public interface IEntityService<T> where T : class, new()
    {
        List<T> GetAll();
        List<T> GetBy(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        void CreateOrUpdate(T item);
        void Delete(int id);
        void Delete(T item);
    }
}
