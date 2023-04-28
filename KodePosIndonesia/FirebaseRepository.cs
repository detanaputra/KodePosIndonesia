using Newtonsoft.Json;

namespace KodePosIndonesia
{
    public class FirebaseRepository<T> : IRepository<T> where T : BaseModel
    {
        private HttpClient httpClient;
        private string indexOn = string.Empty;

        public FirebaseRepository(HttpClient client, string indexOn)
        {
            httpClient = client;
            this.indexOn = indexOn;
        }

        public string IndexOn 
        {
            get { return this.indexOn; }
            set { this.indexOn = value; }
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

        public async Task<IEnumerable<T>> GetAsync(int searchQuery)
        {
            HttpResponseMessage response = await httpClient.GetAsync($".json?orderBy=\"{indexOn}\"&startAt={searchQuery}&endAt={searchQuery}&limitToFirst=100");
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

        public async Task<T?> GetSingleAsync(int id)
        {
            HttpResponseMessage response = await httpClient.GetAsync($".json?orderBy=\"{indexOn}\"&startAt={id}&endAt={id}&limitToFirst=100");
            response.EnsureSuccessStatusCode();
            string? jsonStr = await response.Content.ReadAsStringAsync();
            Dictionary<string, T>? dict = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonStr);
            return dict == null
                ? throw new ArgumentNullException("JsonConvert can't convert the json object from Firebase Realtime Database")
                : dict.ToList().FirstOrDefault();
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
