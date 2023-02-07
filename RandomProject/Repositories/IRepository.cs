using System.Linq.Expressions;
using System.Security.Principal;

namespace RandomProject.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> Add(T entity);
        Task<T> Remove(T entity);
        Task<T> Update(int id, T entity);
        Task<bool> SaveChangesAsync();
        Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> FindListByConditionAsync(Expression<Func<T, bool>> predicate);
    }
}
