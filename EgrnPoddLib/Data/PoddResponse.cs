using EgrnPoddLib.Data.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;

namespace EgrnPoddLib.Data
{
    [Newtonsoft.Json.JsonConverter(typeof(PoddResponseJsonConverter))]
    public class PoddResponse
    {
        //[Newtonsoft.Json.JsonProperty("created_at")]
        public DateTime? CreatedAt { set; get; }
        //[Newtonsoft.Json.JsonProperty("query_id")]
        public string? QueryId { set; get; }
        //[Newtonsoft.Json.JsonProperty("meta")]
        //[Newtonsoft.Json.JsonConverter(typeof(MetaJsonConverter))]
        public List<MetaDataItem> MetaDataItems { set; get; } = new();
        //[Newtonsoft.Json.JsonProperty("rows")]
        //[Newtonsoft.Json.JsonConverter(typeof(RowsJsonConverter))]
        public List<Dictionary<string, object?>> Rows { get; set; } = new();
        //[Newtonsoft.Json.JsonProperty("is_success")]
        public bool? IsSuccess { set; get; }
        //[Newtonsoft.Json.JsonProperty("error")]
        public string? Error { set; get; }
        /*private Dictionary<string, object> parseRowString(List<string> rowsString)
        {
            if (MetaDataItems.Count() != rowsString.Count()) throw new Exception("Colomns number is not equal for Meta and Rows");

            Dictionary<string, object> RowItems = new();
            for(int i =0; i< MetaDataItems.Count();i++)
            {
                RowItems[MetaDataItems[i].ColumnName] = rowsString[i]; // {Name of colomn : value}
                i++;
            }
            return RowItems;
        }
        private List<MetaDataItem> parseMetadata(Dictionary<string,string> metaDict)
        {
            var metaDataItems = new List<MetaDataItem>();
            foreach (var record in metaDict)
            {
                var typeAsString = record.Value;
                Type type;
                switch (typeAsString)
                {
                    case "STRING":
                        type = typeof(int); 
                        break;
                    case "DOUBLE":
                        type = typeof(double);
                        break;
                    case "DATE":
                        type = typeof(DateTime);
                        break;
                    case "TIMESTAMP":
                        type = typeof(DateTime);
                        break;
                    default:
                        type = null; break;
                }
                metaDataItems.Append(new MetaDataItem
                {
                    ColumnName = record.Key,
                    ColumnType = type
                }) ;
            }
            return metaDataItems;
        }
        public PoddResponse(HttpContent responseContent) 
        {
            if (responseContent is null) throw new NullReferenceException("responseContent was null");

            var strContent = responseContent.ReadAsStringAsync().Result;
            //strContent = strContent.Replace("\"","\'");
            //strContent = strContent.Replace("\n", "");
            //var strContent = JObject.Parse(strContent);
            var content = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(strContent);

            if (content is null) throw new NullReferenceException("No content presented.");

            if (content["created_at"] is not null) CreatedAt = content["created_at"].ToObject<DateTime>();
            if (content["query_id"] is not null) QueryId = content["query_id"].ToObject<string>();

            if (content["meta"] is not null)
            {
                MetaDataItems = parseMetadata(
                content["meta"].ToObject<Dictionary<string, string>>()
                );
            }

            if (content["rows"] is not null)
            {
                var rowsList = content["rows"].ToObject<List<List<string>>>();
                foreach (var el in rowsList)
                {
                    Rows.Append(parseRowString(el));
                }
            }

            if(content["is_success"] is not null) IsSuccess = content["is_success"].ToObject<bool>();
            if (content["error"] is not null) Error = content["error"].ToObject<string>();
        }*/
    }
}
