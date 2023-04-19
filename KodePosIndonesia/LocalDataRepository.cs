using CsvHelper;
using CsvHelper.Configuration;

using System.Reflection;

namespace KodePosIndonesia
{
    public class LocalDataRepository<T> : IRepository<T> where T : BaseModel
    {
        private CsvConfiguration configuration;
        private string? csvPath;
        private T? type;

        public LocalDataRepository(CsvConfiguration config, string _cssvPath)
        {
            configuration = config;
            csvPath = _cssvPath;
        }

        public virtual async IAsyncEnumerable<T> GetAllAsync()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream? stream = assembly.GetManifestResourceStream(csvPath);
            using StreamReader reader = new(stream);
            using CsvReader csv = new(reader, configuration);
            IAsyncEnumerable<T> records = csv.GetRecordsAsync<T>();
            await foreach (T record in records)
            {
                yield return record;
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllPolledAsync()
        {
            IAsyncEnumerable<T> records = GetAllAsync();
            return await records.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllPolledAsync(Func<T, bool> predicate)
        {
            IAsyncEnumerable<T> records = GetAllAsync();
            return await records.Where(predicate).ToListAsync();
        }

        public virtual T? GetById(int id)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using Stream? stream = assembly.GetManifestResourceStream(csvPath);
            using StreamReader reader = new(stream);
            using CsvReader csv = new(reader, configuration);
            csv.Read();
            return csv.GetRecord<T>();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            IAsyncEnumerable<T> records = GetAllAsync();
            T? record = await records.FirstOrDefaultAsync(r => r.Id == id);
            return record;
        }
    }
}