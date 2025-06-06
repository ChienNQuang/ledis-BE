using System.Text.Json;
using System.Text.Json.Serialization;
using ledis_BE.Models.List;
using ledis_BE.Models.String;

namespace ledis_BE.Converters;

public class ListValueConverter : JsonConverter<IListValue<IStringValue>>
{
    public override IListValue<IStringValue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        JsonElement root = doc.RootElement;

        ListValueEncoding encoding = (ListValueEncoding)root.GetProperty("Encoding").GetInt32();

        return encoding switch
        {
            ListValueEncoding.LinkedList => root.Deserialize<LinkedListListValue<IStringValue>>(options),
            _ => throw new NotSupportedException($"Unknown IListValue: {encoding}")
        };
    }

    public override void Write(Utf8JsonWriter writer, IListValue<IStringValue> value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case LinkedListListValue<IStringValue> linkedListListValue:
                JsonSerializer.Serialize(writer, linkedListListValue, options);
                break;
            default:
                throw new NotSupportedException($"Cannot serialize {value.GetType()}");
        }
    }
}