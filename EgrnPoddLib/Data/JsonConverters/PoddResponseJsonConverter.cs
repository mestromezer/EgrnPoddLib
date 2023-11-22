using Newtonsoft.Json;
using System.Globalization;

namespace EgrnPoddLib.Data.JsonConverters
{
    public class PoddResponseJsonConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType) => typeof(PoddResponse).FullName == objectType.FullName;

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            reader.Read();// init -> created_at
            var CreatedAt = parseCreatedAt(ref reader);
            reader.Read();// created_at -> query_id
            var QueryId = parseQueryId(ref reader);
            reader.Read();// query_id -> meta
            var MetaDataItems = parseMetaDataItems(ref reader);
            reader.Read(); // meta -> rows
            var Rows = parseRows(ref reader, MetaDataItems);
            reader.Read(); // rows -> is_success
            var IsSuccess = parseIsSuccess(reader);
            reader.Read(); // is_success -> error
            var Error = parseError(reader);

            var newbie = new PoddResponse()
            {
                CreatedAt = CreatedAt,
                QueryId = QueryId,
                MetaDataItems = MetaDataItems,
                Rows = Rows,
                IsSuccess = IsSuccess,
                Error = Error
            };
            return newbie;
        }

        private string? parseError(JsonReader reader)
        {
            if (reader.Value as string != "error") return null;
            return (string)reader.Value;
        }

        private bool? parseIsSuccess(JsonReader reader)
        {
            if (reader.Value as string != "is_issues") return null;
            return (bool)reader.Value;
        }

        private List<Dictionary<string, object?>>? parseRows(ref JsonReader reader, List<MetaDataItem> MetaDataItems)
        {
            if (reader.Value as string != "rows") return null;

            var Rows = new List<Dictionary<string, object?>>();

            reader.Read();

            while (reader.Read() && reader.TokenType != JsonToken.EndArray)
            {
                var it = MetaDataItems.GetEnumerator();
                var row = new Dictionary<string, object?>();
                while (reader.Read() && reader.TokenType != JsonToken.EndArray && it.MoveNext())
                {
                    string name; // Name of type in PODD specification
                    Type type; // Type inside C#
                    try
                    {
                        name = it.Current.ColumnName;
                        type = Type.GetType(it.Current.ColumnType.FullName);
                    }
                    catch (NullReferenceException ex) 
                    {
                        // логгер?
                        throw ex;
                    }
                    object value;
                    if (type.FullName != "System.Double") value = Convert.ChangeType(reader.Value, type);
                    else
                    {
                        if (reader.Value != null)
                        {
                            value = double.Parse((string)reader.Value, CultureInfo.InvariantCulture);
                        }
                        else value = null;
                    }
                    row[name] = value;
                }
                Rows.Add(row);
            }
            return Rows;
        }

        private List<MetaDataItem> parseMetaDataItems(ref JsonReader reader)
        {
            if (reader.Value as string != "meta") return null;
            var MetaDataItems = new List<MetaDataItem>();
            reader.Read(); // meta -> [
            reader.Read(); // [ -> {
            while (reader.TokenType != JsonToken.EndArray)
            {

                var item = new MetaDataItem();

                reader.Read(); // { -> name
                reader.Read(); // name -> value

                item.ColumnName = (string)reader.Value;

                reader.Read(); // value -> type
                reader.Read(); //type -> value

                var typeAsString = reader.Value;
                switch (typeAsString)
                {
                    case "INTEGER": // Чтобы мой единственный запрос отработал)
                        item.ColumnType = typeof(int);
                        break;
                    case "STRING":
                        item.ColumnType = typeof(string);
                        break;
                    case "DOUBLE":
                        item.ColumnType = typeof(double);
                        break;
                    case "DATE":
                        item.ColumnType = typeof(DateTime);
                        break;
                    case "TIMESTAMP":
                        item.ColumnType = typeof(DateTime);
                        break;
                    default:
                        throw new Exception("Невалидное значение type в meta");
                }

                reader.Read(); // value -> }
                reader.Read(); // } -> {
                MetaDataItems.Add(item);
            }
            return MetaDataItems;
        }

        private string parseQueryId(ref JsonReader reader)
        {
            if (reader.Value as string != "query_id") return null;
            reader.Read();
            return (string)reader.Value;
        }

        private DateTime? parseCreatedAt(ref JsonReader reader)
        {
            if (reader.Value as string != "created_at") return null;
            reader.Read();
            return (DateTime)reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}