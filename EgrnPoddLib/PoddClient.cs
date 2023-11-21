using EgrnPoddLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgrnPoddLib
{
    public class PoddClient
    {
        private const string _defaultEndpoint = "http://localhost:8192";
        private readonly HttpClient _httpClient;
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
        public HttpClient CreateClient(string? endpoint)
        {
            string endpointAddress = endpoint ?? _defaultEndpoint;
            var client = new HttpClient();
            client.BaseAddress = new Uri(endpointAddress);

            return client;
        }
        public PoddResponse SendRequest(string request)
        {
            
        }
    }
}
