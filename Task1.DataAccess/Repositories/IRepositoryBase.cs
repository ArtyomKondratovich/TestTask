using System.Linq.Expressions;

namespace Task1.DataAccess.Repositories
{
    public interface IRepositoryBase<T>
    {
        Task<T?> CreateAsync(T entity, CancellationToken token = default);
        Task<T?> UpdateAsync(T entity, CancellationToken token = default);
        Task<bool> DeleteAsync(T entity, CancellationToken token = default);
        Task<T?> GetByIdAsync(int id, CancellationToken token = default);
        Task<T?> GetByPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default);
        Task<IEnumerable<T>> GetByFilterAsync(Expression<Func<T, bool>> filter, CancellationToken token = default);
    }
}
