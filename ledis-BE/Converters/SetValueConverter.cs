using System.Text.Json;
using System.Text.Json.Serialization;
using ledis_BE.Models.Set;
using ledis_BE.Models.String;

namespace ledis_BE.Converters;

public class SetValueConverter : JsonConverter<ISetValue<IStringValue>>
{
    public override ISetValue<IStringValue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        JsonElement root = doc.RootElement;

        SetValueEncoding encoding = (SetValueEncoding)root.GetProperty("Encoding").GetInt32();

        return encoding switch
        {
            SetValueEncoding.Hashtable => root.Deserialize<HashSetSetValue<IStringValue>>(options),
            _ => throw new NotSupportedException($"Unknown ISetValue: {encoding}")
        };
    }

    public override void Write(Utf8JsonWriter writer, ISetValue<IStringValue> value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case HashSetSetValue<IStringValue> hashSetSetValue:
                JsonSerializer.Serialize(writer, hashSetSetValue, options);
                break;
            default:
                throw new NotSupportedException($"Cannot serialize {value.GetType()}");
        }
    }
}