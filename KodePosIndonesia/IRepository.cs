namespace KodePosIndonesia
{
    public interface IRepository<T> : IDisposable where T : BaseModel
    {
        string IndexOn { get; set; }
        Task<IEnumerable<T>> GetAsync();
        Task<IEnumerable<T>> GetAsync(int searchQuery);
        Task<T> GetSingleAsync(string recordId);
        Task<T> GetSingleAsync(int id);
    }
}