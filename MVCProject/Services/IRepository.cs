using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Services
{
    public interface IRepository<T>
    {
        IQueryable<T> Select();
        T Select(int id);
        void Insert(T item);
        void Insert(IEnumerable<T> items);
        void Delete(T item);
        void Update(T item);
        void Save();
    }
}
