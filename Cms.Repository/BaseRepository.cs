using Cms.Entity;
using Cms.EntityFramewrokCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Repository
{
    public class BaseRepository<T> where T : class, new()
    {
        protected MyDbContext myDbContext;
        public BaseRepository(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }

        public async Task<bool> DeleteEntityAsync(T entity)
        {
            //throw new NotImplementedException();
            myDbContext.Set<T>().Remove(entity);
            //return await myDbContext.SaveChangesAsync()>0;
            return await Task.FromResult(true);
        }

        public async Task<bool> InsertEntityAsync(T entity)
        {
            myDbContext.Set<T>().Add(entity);
            return await Task.FromResult(true);
        }

        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            return myDbContext.Set<T>().Where(whereLambda);
        }

        public IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int toalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderbyLambda, bool isAsc)
        {
            var temp = myDbContext.Set<T>().Where<T>(whereLambda);
            toalCount = temp.Count();
            if (isAsc)
            {
                //升序排序
                temp = temp.OrderBy(orderbyLambda).Skip((pageIndex-1)*pageSize).Take(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending(orderbyLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            return temp;
        }

        public Task<bool> UpdateEntityAsync(T entity)
        {
            myDbContext.Set<T>().Update(entity);
            return Task.FromResult(true);
        }
    }
}
