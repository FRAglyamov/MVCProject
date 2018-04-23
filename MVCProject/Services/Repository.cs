using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MVCProject.Services
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> DbSet;

        protected Repository(DbSet<T> dbSet)
        {
            DbSet = dbSet;
        }

        ~Repository()
        {
            Save();
        }

        public void Delete(T item)
        {
            DbSet.Remove(item);
        }

        public void Insert(T item)
        {
            DbSet.Add(item);
        }

        public void Insert(IEnumerable<T> items)
        {
            DbSet.AddRange(items);
        }

        public abstract void Save();

        public IQueryable<T> Select() => DbSet;

        public T Select(int id) => DbSet.Find(id);

        public void Update(T item)
        {
            DbSet.Update(item);
        }
    }
}
