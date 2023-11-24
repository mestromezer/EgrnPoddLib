﻿using EgrnPoddLib.PoddClient.Data;
using Newtonsoft.Json;
using System.Globalization;
using JsonException = Newtonsoft.Json.JsonException;

namespace EgrnPoddLib.PoddClient.JsonConverters;
public class PoddResponseJsonConverter : JsonConverter<SmevResponse>
{
    public override SmevResponse? ReadJson(JsonReader reader, Type objectType, SmevResponse? existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
            throw new JsonException("Json должен содержать информацию об объекте");
        var obj = new SmevResponse()
        { IsSuccess = true};
        try
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject)
                    return obj;
                if (reader.TokenType != JsonToken.PropertyName)
                    continue;
                var propertyName = (string?)reader.Value;
                reader.Read();
                switch (propertyName)
                {
                    case "created_at":
                        obj.CreatedAt = (DateTime?)reader.Value;
                        break;
                    case "query_id":
                        obj.QueryId = (string?)reader.Value;
                        break;
                    case "error":
                        obj.IsSuccess = false;
                        obj.Error = (string?)reader.Value;
                        break;
                    case "meta":
                        if (reader.TokenType != JsonToken.StartArray)
                            throw new JsonException("Элемент meta должен быть массивом");
                        reader.Read();
                        obj.MetaDataItems = parseMetaDataItems(reader);
                        break;
                    case "rows":
                        if (reader.TokenType != JsonToken.StartArray)
                            throw new JsonException("Элемент rows должен быть массивом");
                        obj.Rows = parseRows(reader, obj.MetaDataItems);
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            obj.Error = $"Ошибка преобразования ответа: {ex.Message}";
            obj.IsSuccess = false;
        }
        return obj;
    }
    private List<Dictionary<string, object?>> parseRows(JsonReader reader, List<MetaDataItem> MetaDataItems)
    {
        var Rows = new List<Dictionary<string, object?>>();

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
                    type = it.Current.ColumnType.FullName != null ? Type.GetType(it.Current.ColumnType.FullName):default;
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
        var MetaDataItems = new List<MetaDataItem>();
        while (reader.TokenType != JsonToken.EndArray)
        {
            string ColumnName = string.Empty;
            Type ColumnType = default!;

            reader.Read(); // { -> name
            reader.Read(); // name -> value

            if(reader.Value != null) ColumnName = (string)reader.Value;

            reader.Read(); // value -> type
            reader.Read(); //type -> value

            var typeAsString = reader.Value;
            switch (typeAsString)
            {
                case "INTEGER": // Чтобы мой единственный запрос отработал)
                    ColumnType = typeof(int);
                    break;
                case "STRING":
                    ColumnType = typeof(string);
                    break;
                case "DOUBLE":
                    ColumnType = typeof(double);
                    break;
                case "DATE":
                    ColumnType = typeof(DateTime);
                    break;
                case "TIMESTAMP":
                    ColumnType = typeof(DateTime);
                    break;
                default:
                    throw new Exception("Невалидное значение type в meta");
            }
            var item = new MetaDataItem(ColumnName:ColumnName, ColumnType:ColumnType);
            reader.Read(); // value -> }
            reader.Read(); // } -> {
            MetaDataItems.Add(item);
        }
        return MetaDataItems;
    }
    public override void WriteJson(JsonWriter writer, SmevResponse? value, Newtonsoft.Json.JsonSerializer serializer)
    {
        throw new NotImplementedException("Сериализация данного класса не реализована");
    }
}