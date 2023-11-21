namespace EgrnPoddLib.Data
{
    public class PoddResponse
    {
        public HttpContent Data { get; set; }
        public async Task<string> getContentAsStringAsync() => await Data.ReadAsStringAsync();
    }
}
