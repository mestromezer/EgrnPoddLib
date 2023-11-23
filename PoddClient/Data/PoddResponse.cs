using EgrnPoddLib.PoddClient.JsonConverters;

namespace EgrnPoddLib.PoddClient.Data
{
    [Newtonsoft.Json.JsonConverter(typeof(PoddResponseJsonConverter))]
    public class PoddResponse
    {
        public DateTime? CreatedAt { set; get; }
        public string? QueryId { set; get; }
        public List<MetaDataItem> MetaDataItems { set; get; } = new();
        public List<Dictionary<string, object?>> Rows { get; set; } = new();
        public bool? IsSuccess { set; get; }
        public string? Error { set; get; }
    }
}
