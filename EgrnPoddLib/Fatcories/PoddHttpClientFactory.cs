namespace EgrnPoddLib.Fatcories
{
    public class PoddHttpClientFactory : IHttpClientFactory
    {
        private const string DefaultEndpoint = "http://192.168.1.40:8192"; // вытащить в конфиг?
        private HttpClient ConfigureCustomClient(string? endpoint = null)
        {
            string endpointAddress = endpoint ?? DefaultEndpoint;
            var client = new HttpClient();
            client.BaseAddress = new Uri(endpointAddress);
            client.Timeout = new TimeSpan(0, 0, 8); // Timeout 8 secs


            client.DefaultRequestHeaders.Add("Accept-Version", "1");

            return client;
        }
        public HttpClient CreateClient(string name)
        {
            if (name != "default") throw new ArgumentException("Неверное логическое имя HttpCleint для PoddHttpClientFactory");
            return ConfigureCustomClient();
        }
        public HttpClient CreateClient(string name, string? endpoint)
        {
            if (name != "default") throw new ArgumentException("Неверное логическое имя HttpCleint для PoddHttpClientFactory");
            return ConfigureCustomClient(endpoint);
        }
    }
}
