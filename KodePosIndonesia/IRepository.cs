namespace KodePosIndonesia
{
    public interface IRepository<T> where T : BaseModel
    {
        IAsyncEnumerable<T> GetAllAsync();
        Task<IEnumerable<T>> GetAllPolledAsync();
        Task<IEnumerable<T>> GetAllPolledAsync(Func<T, bool> predicate);
        T? GetById(object id);
        Task<T> GetByIdAsync(object id);
    }
}