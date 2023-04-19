using Newtonsoft.Json;

namespace KodePosIndonesia.FirebaseHttpClients
{
    internal interface ICityClient
    {
        Task<IEnumerable<CityModel>> GetAsync();
        Task<IEnumerable<CityModel>> GetAsync(int parentId);
        Task<CityModel> GetSingleAsync(string recordId);
    }

    internal class CityClient<T> : IFirebaseClient, ICityClient
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _baseUrl = new Uri("https://kodepos-c2535-default-rtdb.asia-southeast1.firebasedatabase.app/Cities");

        public CityClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task<string> GetStringFromUrl(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<IEnumerable<CityModel>> GetAsync()
        {
            string jsonStr = await GetStringFromUrl($"{_baseUrl}.json?limitToFirst=100");
            return JsonConvert.DeserializeObject<IEnumerable<CityModel>>(jsonStr);
        }

        public async Task<IEnumerable<CityModel>> GetAsync(int parentId)
        {
            string jsonStr = await GetStringFromUrl($"{_baseUrl}.json?oderBy=ProvinceId&startAt={parentId}&endAt={parentId}&limitToFirst=100");
            return JsonConvert.DeserializeObject<IEnumerable<CityModel>>(jsonStr);
        }

        public async Task<CityModel> GetSingleAsync(string recordId)
        {
            string jsonStr = await GetStringFromUrl($"{_baseUrl}/{recordId}.json?");
            return JsonConvert.DeserializeObject<CityModel>(jsonStr);
        }
    }
}
