using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assignment1.Api.Converters
{
    public class GuidJsonConverter : JsonConverter<Guid>
    {
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && Guid.TryParse(reader.GetString(), out var guid))
            {
                return guid;
            }

            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException($"Invalid token type of {reader.TokenType} for Guid conversion. Expected a valid GUID string.");
            }

            throw new JsonException($"Invalid GUID format: {reader.GetString()}");
        }

        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
