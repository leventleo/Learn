using LearnCode.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LearnCode.DataAccess
{
    public class RepositoryBase<T,TContext> : IRepository<T> where T : EntitBase,IEntity, new()
        where TContext:DbContext,new()
    {
        protected static TContext _context;
                
        public DbContext Context
        {
            get
            {

                if (_context == null)
                {
                    return _context = new TContext();
                }

                return _context;
            }
            
        }


        public T Add(T entity)
        {

            Context.Set<T>().Add(entity);
            return entity;
        }

        public T FindById(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public virtual async Task<List<T>> GetList(Expression<Func<T, bool>> filter = null)
        {
            if (filter==null)
            {
               return await  Context.Set<T>().ToListAsync();
            }
            else
            {
               return await  Context.Set<T>().Where(filter).ToListAsync();
            }
        }

        public T Remove(T entity)
        {
            //Context.Set<T>().Remove(entity);
            Update(entity);
            return entity;
        }

        public int Save()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception ex)
            {

                return 0;
            }
          
        }

        public IQueryable<T> Table(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
            {
                    return Context.Set<T>().AsNoTracking();
                                
            }
            else
            {
              return Context.Set<T>().Where(filter).AsNoTracking();
            }
        }

        public T Update(T entity)
        {
            entity.Tombstone = DateTime.Now;
            Context.Set<T>().Update(entity);
            return entity;
        }

       

    }
}
