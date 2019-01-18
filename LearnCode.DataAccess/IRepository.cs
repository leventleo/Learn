using LearnCode.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LearnCode.DataAccess
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        T Add(T entity);
        T FindById(int id);
       Task<List<T>> GetList(Expression<Func<T, bool>> filter = null);
        T Remove(T entity);
        int Save();
       IQueryable<T>Table(Expression<Func<T, bool>> filter = null);
        T Update(T entity);

    }
}
