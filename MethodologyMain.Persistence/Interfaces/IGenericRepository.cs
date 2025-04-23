using System.Linq.Expressions;

namespace MethodologyMain.Persistence.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken token);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken token);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, CancellationToken token);
        Task AddAsync(T entity, CancellationToken token);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken token);
        Task RemoveAsync(T entity, CancellationToken token);
        Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken token);
    }
}
