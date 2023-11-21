using EgrnPoddLib.Data;
using EgrnPoddLib.Data.RequestBody;
using Newtonsoft.Json;
using System.Text;

namespace EgrnPoddLib
{
    public class PoddClient
    {
        private const string _defaultEndpoint = "http://192.168.1.40:8192";
        private readonly HttpClient _httpClient;
        /*private string formatAsRequestBody(StringBuilder request)
        {
            request.Insert(0, "{\"sql\": {\"sql\": ");
            request.Append("}}");
            return request.ToString();
        }*/
        private HttpClient CreateClient(string? endpoint)
        {
            string endpointAddress = endpoint ?? _defaultEndpoint;
            var client = new HttpClient();
            client.BaseAddress = new Uri(endpointAddress);

            client.DefaultRequestHeaders.Add("Accept-Version", "1");

            return client;
        }
        public PoddClient(HttpClient? client, string? endpointAddress)
        {
            if (client is not null)
            {
                if (endpointAddress is not null)
                {
                    _httpClient = client;
                    _httpClient.BaseAddress = new Uri(endpointAddress);
                }
                else _httpClient = client; 
            }
            else _httpClient = CreateClient(endpointAddress);
        }
        public async Task<PoddResponse> SendRequestAsync(string request)
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
                var poddResponse = new PoddResponse
                {
                    Data = responseMessage.Content
                };

                return poddResponse;
            }
        }
    }
}
