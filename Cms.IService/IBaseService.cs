using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cms.IService
{
    public interface IBaseService<T> where T : class, new()
    {
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int toalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderbyLambda, bool isAsc);

        Task<bool> DeleteEntityAsync(T entity);
        Task<bool> UpdateEntityAsync(T entity);
        Task<bool> InsertEntityAsync(T entity);
    
    }
}
