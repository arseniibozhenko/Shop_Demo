using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Demo.DAL.Repositories
{
    public interface IRepository<T> where T : class 
    {
        List<T> GetAll();

        List<T> GetBy(Expression<Func<T, bool>> predicate);

        T GetById(int id);

        void CreateOrUpdate(T item);

        void Update(T item);

        void Delete(int id);

        void Delete(T item);

        void Save();
    }
}
