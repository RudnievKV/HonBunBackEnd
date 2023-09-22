using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace HonbunNoAnkiApi.Repositories.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void CreateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}
