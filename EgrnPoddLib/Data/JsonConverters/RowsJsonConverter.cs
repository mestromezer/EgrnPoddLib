using Newtonsoft.Json;
using System.Globalization;

namespace EgrnPoddLib.Data.JsonConverters
{
    public class RowsJsonConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var Rows = new List<Dictionary<string, object?>>();
            reader.Read();
            while (reader.TokenType != JsonToken.EndArray)
                {
                    var row = new Dictionary<string, object?>();
                    while (reader.TokenType != JsonToken.EndArray)
                    {
                        reader.Read();
                        row["realestates_cad_number"] = (string)reader.Value;

                        reader.Read();
                        row["realestates_realestate_type_value"] = (string)reader.Value;

                        reader.Read();
                        row["realestates_address_readable_address"] = (string)reader.Value;

                        reader.Read();
                        if ((string)reader.Value != null)
                        {
                            var doubleD = double.Parse((string)reader.Value, CultureInfo.InvariantCulture);
                            row["realestates_area_value"] = doubleD;
                        }

                        reader.Read();
                        row["right_holder_legacy_entities_ogrn"] = (string)reader.Value;

                        reader.Read();
                        row["right_holder_legacy_entities_norm_inn"] = (string)reader.Value;

                        reader.Read();
                        row["right_holder_legacy_entities_norm_kpp"] = (string)reader.Value;

                        reader.Read();
                        row["right_holder_legacy_entities_registration_number"] = (string)reader.Value;

                        reader.Read();
                    }
                    Rows.Add(row);
                    reader.Read();
                }
                return Rows;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}