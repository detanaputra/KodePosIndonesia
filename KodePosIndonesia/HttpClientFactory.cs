namespace KodePosIndonesia
{
    internal class HttpClientFactory : IHttpClientFactory
    {
        private static HttpClient _httpClient;

        public HttpClient CreateClient(string name)
        {
            _httpClient ??= new HttpClient();
            return _httpClient;
        }
    }
}
