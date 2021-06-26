using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>>GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T,bool>> predicate);
        Task<T> GetById(object id);
        Task<T> Insert(T obj);
        Task<T> Update(T obj);
        Task Delete(object id);
        Task Save();
    }
}
