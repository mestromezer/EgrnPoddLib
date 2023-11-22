using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EgrnPoddLib.Data.JsonConverters
{
    internal class MetaJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            var t = objectType;
            return t == typeof(PoddResponse);
        }

        public override List<MetaDataItem> ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var MetaDataItems = new List<MetaDataItem>();
            reader.Read();
            while (reader.TokenType != JsonToken.EndArray)
            {
                reader.Read();
                var item = new MetaDataItem();
                var data = (string)reader.Value;

                if (data == "name")
                {
                    reader.Read();
                    item.ColumnName = (string)reader.Value;
                }
                reader.Read();
                data = (string)reader.Value;
                if (data == "type")
                {
                    reader.Read();
                    var typeAsString = reader.Value;
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
                            throw new Exception("Невалидное значение type в meta");
                    }
                }

                reader.Read();
                reader.Read();
                MetaDataItems.Add(item);
            }
            return MetaDataItems;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}