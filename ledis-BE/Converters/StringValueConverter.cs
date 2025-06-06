using System.Text.Json;
using System.Text.Json.Serialization;
using ledis_BE.Models.String;

namespace ledis_BE.Converters;

public class StringValueConverter : JsonConverter<IStringValue>
{
    public override IStringValue? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        JsonElement root = doc.RootElement;

        StringValueEncoding encoding = (StringValueEncoding)root.GetProperty("Encoding").GetInt32();

        return encoding switch
        {
            StringValueEncoding.Raw => root.Deserialize<RawStringValue>(options),
            StringValueEncoding.Int => root.Deserialize<IntStringValue>(options),
            _ => throw new NotSupportedException($"Unknown IStringValue: {encoding}")
        };
    }

    public override void Write(Utf8JsonWriter writer, IStringValue value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case RawStringValue rawStringValue:
                JsonSerializer.Serialize(writer, rawStringValue, options);
                break;
            case IntStringValue intStringValue:
                JsonSerializer.Serialize(writer, intStringValue , options);
                break;
            default:
                throw new NotSupportedException($"Cannot serialize {value.GetType()}");
        }
    }
}