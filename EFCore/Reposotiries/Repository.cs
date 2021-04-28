using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFCore.Reposotiries
{
    public class Repository<T> : IRespository<T> where T :class
    {
        protected readonly postgresContext _db;
        protected readonly DbSet<T> _dbSet;
        public Repository(postgresContext db)
        {
            _db = db;
            if(_db != null)
            {
                _dbSet = _db.Set<T>();
            }
        }
        public virtual T Insert(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }
         var newEntity = _db.Set<T>().Add(entity).Entity;
            return newEntity;
        }
        public void Update(T entity)
        {
            _db.Set<T>().Update(entity);
        }
        public virtual void Delete(object id)
        {
            var entityToDelete = _dbSet.Find(id);
            this.Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {           
            _dbSet.Remove(entityToDelete);
        }
        public virtual T GetById(object id)
        {           
            return _db.Set<T>().Find(id);
        }
        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }
        public virtual int SaveChanges()
        {
            return _db.SaveChanges();
        }
    }
}
