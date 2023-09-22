using HonbunNoAnkiApi.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HonbunNoAnkiApi.DBContext;

namespace HonbunNoAnkiApi.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly MyDBContext _dbContext;
        public GenericRepo(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }
        public void CreateRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression);
        }
        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbContext.ChangeTracker.Clear();
            _dbContext.Set<T>().Update(entity);
        }
    }
}
