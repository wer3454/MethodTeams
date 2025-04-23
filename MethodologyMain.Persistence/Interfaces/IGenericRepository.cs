using System.Linq.Expressions;

namespace MethodologyMain.Persistence.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity, CancellationToken token = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken token = default);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, CancellationToken token = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default);
        Task<T?> GetByIdAsync(Guid id, CancellationToken token = default);
        Task RemoveAsync(T entity, CancellationToken token = default);
        Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken token = default);
    }
}
