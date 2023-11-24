using EgrnPoddLib.Fatcories;
using EgrnPoddLib.PoddClient.Data;
using Newtonsoft.Json;
using System.Text;

namespace EgrnPoddLib.PoddClient
{
    public class PoddClient
    {
        private readonly HttpClient _httpClient;
        public PoddClient(string? endpointAddress=null) 
        {
            var clientFactory = new PoddHttpClientFactory();
            _httpClient = clientFactory.CreateClient("default",endpointAddress);
        }
        public PoddClient(HttpClient client)
        {
            _httpClient = client;
        }
        public async Task<SmevResponse> SendRequest(string request)
        {
            var requestBody = new requestForm(new request(request));
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
