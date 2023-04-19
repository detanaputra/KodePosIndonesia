namespace KodePosIndonesia.FirebaseHttpClients
{
    public interface IFirebaseClient<T>
    {
        Task<IEnumerable<T>> GetAsync();
        Task<IEnumerable<T>> GetAsync(int parentId);
        Task<T> GetSingleAsync(string recordId);
    }
}