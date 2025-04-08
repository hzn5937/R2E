using Assignment2.Domain.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assignment2.Api.Helpers
{
    public class GenderJsonConverter : JsonConverter<Gender>
    {
        public override Gender Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonValue = reader.GetString();

            if (string.IsNullOrWhiteSpace(jsonValue))
            {
                throw new JsonException("Gender cannot be null or empty.");
            }

            if (Enum.TryParse<Gender>(jsonValue, true, out var gender))
            {
                return gender;
            }

            throw new JsonException($"Invalid gender value: '{jsonValue}'");
        }

        public override void Write(Utf8JsonWriter writer, Gender value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString().ToLower());
        }
    }
}
