using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

public class JsonDateTimeConverter : JsonConverter<DateTime?>
{
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null; // Handle null case for nullable DateTime
        }

        // Parse the DateTime in the desired format
        return DateTime.ParseExact(reader.GetString(), "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value.ToString("dd/MM/yyyy hh:mm tt").ToLower());
        }
        else
        {
            writer.WriteNullValue(); // Handle null value for nullable DateTime
        }
    }
}
