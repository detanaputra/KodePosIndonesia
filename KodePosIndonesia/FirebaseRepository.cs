using KodePosIndonesia.FirebaseHttpClients;

namespace KodePosIndonesia
{
    public class FirebaseRepository<T> : IRepository<T> where T : BaseModel
    {

        private IFirebaseClient<T> firebaseClient;

        public FirebaseRepository(IFirebaseClient<T> client)
        {
            firebaseClient = client;
        }

        [Obsolete("use GetAllPolledAsync() instead.")]
        public async IAsyncEnumerable<T> GetAllAsync()
        {
            IEnumerable<T>? records = await GetAllPolledAsync();
            foreach(T record in records)
            {
                yield return record;
            }
        }

        public async Task<IEnumerable<T>> GetAllPolledAsync()
        {
            return await firebaseClient.GetAsync();
        }

        [Obsolete("Not implemented in firebase", true)]
        public Task<IEnumerable<T>> GetAllPolledAsync(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        [Obsolete("Not implemented in firebase. Use GetByIdAsync instead", true)]
        public T? GetById(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            string strid = id.ToString();
            return await firebaseClient.GetSingleAsync(strid);
        }
    }
}
