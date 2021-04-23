using Shop_Demo.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Shop_Demo.DAL.Repositories
{
    public class GenericRepository<T> : IDisposable, IRepository<T> where T : class
    {
        private DbContext context;
        private DbSet<T> db;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.db = context.Set<T>();
        }

        public List<T> GetAll()
        {
            return db.ToList();
        }

        public List<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return db.Where(predicate).ToList();
        }

        public T GetById(int id)
        {
            return db.Find(id);
        }

        public void CreateOrUpdate(T item)
        {
            //db.Add(good);
            db.AddOrUpdate(item);
            context.SaveChanges();
        }

        public void Update(T item)
        {
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            T item = db.Find(id);
            db.Remove(item);
            context.SaveChanges();
        }

        public void Delete(T item)
        {
            db.Remove(item);
            context.SaveChanges();
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