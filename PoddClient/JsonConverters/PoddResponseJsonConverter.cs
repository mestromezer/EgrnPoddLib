using EgrnPoddLib.PoddClient.Data;
using Newtonsoft.Json;
using System.Globalization;

namespace EgrnPoddLib.PoddClient.JsonConverters
{
    public class PoddResponseJsonConverter : JsonConverter<PoddResponse>
    {
        public override PoddResponse? ReadJson(JsonReader reader, Type objectType, PoddResponse? existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)

        {
            reader.Read();// init -> created_at
            var CreatedAt = parseCreatedAt(reader);
            reader.Read();// created_at -> query_id
            var QueryId = parseQueryId(reader);
            reader.Read();// query_id -> meta
            var MetaDataItems = parseMetaDataItems(reader);
            reader.Read(); // meta -> rows
            var Rows = parseRows(reader, MetaDataItems);
            reader.Read(); // rows -> error
            var Error = parseError(reader);

            bool IsSuccess = true;
            if (Error is null) IsSuccess = false;

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
            reader.Read();
            return (string)reader.Value;
        }

        private List<Dictionary<string, object?>>? parseRows(JsonReader reader, List<MetaDataItem> MetaDataItems)
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

        private List<MetaDataItem> parseMetaDataItems(JsonReader reader)
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
                    //case "INTEGER": // Чтобы мой единственный запрос отработал)
                    //    item.ColumnType = typeof(int);
                    //    break;
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

        private string parseQueryId(JsonReader reader)
        {
            if (reader.Value as string != "query_id") return null;
            reader.Read();
            return (string)reader.Value;
        }

        private DateTime? parseCreatedAt(JsonReader reader)
        {
            if (reader.Value as string != "created_at") return null;
            reader.Read();
            return (DateTime)reader.Value;
        }

        public override void WriteJson(JsonWriter writer, PoddResponse? value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException("Сериализация данного класса не реализована");
        }
    }
}