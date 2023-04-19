using Microsoft.EntityFrameworkCore;

namespace KodePosIndonesia
{
    public class SQLiteRepository<T> : IRepository<T> where T : BaseModel
    {
        private DbSet<T> _dbSet;

        public SQLiteRepository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        public IAsyncEnumerable<T> GetAllAsync()
        {
            return _dbSet.AsAsyncEnumerable();
        }

        public async Task<IEnumerable<T>> GetAllPolledAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllPolledAsync(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
