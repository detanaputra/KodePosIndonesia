using Newtonsoft.Json;

namespace KodePosIndonesia
{
    public class FirebaseRepository<T> : IRepository<T> where T : BaseModel
    {
        private HttpClient httpClient;
        private string indexpath = string.Empty;

        public FirebaseRepository(HttpClient client, string indexpath)
        {
            httpClient = client;
            this.indexpath = indexpath;
        }

        public void Dispose() => httpClient?.Dispose();

        public async Task<IEnumerable<T>> GetAsync()
        {
            HttpResponseMessage response = await httpClient.GetAsync($".json");
            response.EnsureSuccessStatusCode();
            string? jsonStr = await response.Content.ReadAsStringAsync();
            Dictionary<string, T>? dict = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonStr);
            return dict == null
                ? throw new ArgumentNullException("JsonConvert can't convert the json object from Firebase Realtime Database")
                : dict.ToList();
        }

        public async Task<IEnumerable<T>> GetAsync(int parentId)
        {
            HttpResponseMessage response = await httpClient.GetAsync($".json?orderBy=\"{indexpath}\"&startAt={parentId}&endAt={parentId}&limitToFirst=100");
            response.EnsureSuccessStatusCode();
            string? jsonStr = await response.Content.ReadAsStringAsync();
            Dictionary<string, T>? dict = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonStr);
            return dict == null
                ? throw new ArgumentNullException("JsonConvert can't convert the json object from Firebase Realtime Database")
                : dict.ToList();
        }

        public async Task<T> GetSingleAsync(string recordId)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{recordId}.json");
            response.EnsureSuccessStatusCode();
            string? jsonStr = await response.Content.ReadAsStringAsync();
            T? obj = JsonConvert.DeserializeObject<T>(jsonStr);
            return obj ?? throw new ArgumentNullException("JsonConvert can't convert the json object from Firebase Realtime Database");
        }
    }

    static class DictionaryToListConverter
    {
        internal static IEnumerable<T> ToList<T>(this Dictionary<string, T> dictionary)
        {
            List<T> list = new();
            foreach (T value in dictionary.Values)
            {
                list.Add(value);
            }
            return list;
        }
    }
}
