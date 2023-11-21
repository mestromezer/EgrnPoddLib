using System.Text.Json;

namespace EgrnPoddLib.Data
{
    public class PoddResponse
    {
        public DateTime CreatedAt { init; get; }
        public string QueryId { init; get; }
        // "name" : "type" pairs
        public Dictionary<string,string> MetaDataItems { init; get; }
        // string double date timestamp
        public List<string> Rows { init; get; }
        public PoddResponse(HttpContent responseContent) 
        {
            if (responseContent is null) throw new NullReferenceException("ResponseContent was null");
            // TODO: Выстакиваем как JSON, сериализация в поля с данными
        }
        public HttpContent Data { get; set; }
    }
}
