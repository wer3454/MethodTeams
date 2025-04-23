using MethodologyMain.Persistence.Interfaces;
using MethodTeams.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MethodologyMain.Persistence.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly MyDbContext _context;
        public GenericRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity, CancellationToken token)
        {
            await _context.Set<T>().AddAsync(entity, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken token)
        {
            await _context.Set<T>().AddRangeAsync(entities, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, CancellationToken token)
        {
            return await _context.Set<T>().Where(expression).ToListAsync(token);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken token)
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync(token);
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken token)
        {
            return await _context.Set<T>().FindAsync(id, token);
        }

        public async Task RemoveAsync(T entity, CancellationToken token)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(token);
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken token)
        {
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync(token);
        }
    }
}
