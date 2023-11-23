using EgrnPoddLib.PoddClient.Data;
using Newtonsoft.Json;
using System.Text;

namespace EgrnPoddLib.PoddClient
{
    public class PoddClient
    {
        private const string _defaultEndpoint = "http://192.168.1.40:8192"; // вытащить в конфиг?
        private readonly HttpClient _httpClient;
        private HttpClient CreateClient(string? endpoint)
        {
            string endpointAddress = endpoint ?? _defaultEndpoint;
            var client = new HttpClient();
            client.BaseAddress = new Uri(endpointAddress);
            client.Timeout = new TimeSpan(0,0,8); // Timeout 8 secs

            client.DefaultRequestHeaders.Add("Accept-Version", "1");

            return client;
        }
        public PoddClient(string? endpointAddress=null) 
        {
            _httpClient = CreateClient(endpointAddress);
        }
        public PoddClient(HttpClient client)
        {
            _httpClient = client;
        }
        public async Task<SmevResponse> SendRequestAsync(string request)
        {
            var requestBody = new requestForm
            {
                sql = new request
                {
                    sql = request,
                }
            };
            var stringPayload = JsonConvert.SerializeObject(requestBody);

            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");


            using (var responseMessage = await _httpClient.PostAsync( "/query", content:httpContent))
            {
                var poddResponseRow = await responseMessage.Content.ReadAsStringAsync();
                var poddResponse = JsonConvert.DeserializeObject<SmevResponse>(poddResponseRow);

                return poddResponse;
            }
        }
    }
}
