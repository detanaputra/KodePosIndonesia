using Newtonsoft.Json;

namespace KodePosIndonesia.FirebaseHttpClients
{
    internal interface IProvinceClient
    {
        Task<IEnumerable<ProvinceModel>> GetAsync();
        Task<IEnumerable<ProvinceModel>> GetAsync(int parentId);
    }

    internal class ProvinceClient<T> : IProvinceClient, IFirebaseClient where T : ProvinceModel
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _baseUrl = new Uri("https://kodepos-c2535-default-rtdb.asia-southeast1.firebasedatabase.app/Provinces");

        public ProvinceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}.json?limitToFirst=100");
            response.EnsureSuccessStatusCode();
            string jsonStr = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonStr);
        }

        public async Task<IEnumerable<T>> GetAsync(int parentId)
        {
            return await GetAsync();
        }
    }
}
