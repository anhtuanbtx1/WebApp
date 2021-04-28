using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IRespository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Insert(T entity);
        void Update(T entity);
        int SaveChanges();
        void Delete(T entityToDelete);
        T GetById(object id);
    }
}
