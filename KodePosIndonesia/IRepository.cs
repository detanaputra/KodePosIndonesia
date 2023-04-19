namespace KodePosIndonesia
{
    public interface IRepository<T> : IDisposable where T : BaseModel
    {
        Task<IEnumerable<T>> GetAsync();
        Task<IEnumerable<T>> GetAsync(int parentId);
        Task<T> GetSingleAsync(string recordId);
    }
}