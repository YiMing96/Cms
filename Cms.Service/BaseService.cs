using Cms.IRepository;
using Cms.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Service
{
    public class BaseService<T> where T : class, new()
    {
        protected IBaseRepository<T> repository { get; set; }
        public Task<bool> DeleteEntityAsync(T entity)
        {
            return repository.DeleteEntityAsync(entity);
        }

        public Task<bool> InsertEntityAsync(T entity)
        {
            return repository.InsertEntityAsync(entity);
        }

        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            return repository.LoadEntities(whereLambda);
        }

        public IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int toalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderbyLambda, bool isAsc)
        {
            return repository.LoadPageEntities(pageIndex, pageSize, out toalCount, whereLambda, orderbyLambda, isAsc);
        }

        public Task<bool> UpdateEntityAsync(T entity)
        {
            return repository.UpdateEntityAsync(entity);
        }
    }
}
