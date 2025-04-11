using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assignment2.Api.Helpers
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && DateOnly.TryParse(reader.GetString(), out var date))
            {
                return date;
            }

            throw new JsonException("Invalid date format. Try 'YYYY/MM/DD'");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
        }
    }
}
