using System.Text.Json;
using System.Text.Json.Serialization;
using ledis_BE.Models;
using ledis_BE.Models.List;
using ledis_BE.Models.Set;
using ledis_BE.Models.String;

namespace ledis_BE.Converters;

public class LedisValueConverter : JsonConverter<LedisValue>
{
    public override LedisValue? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        JsonElement root = doc.RootElement;

        LedisValueType type = (LedisValueType)root.GetProperty("Type").GetInt32();

        return type switch
        {
            LedisValueType.String => root.Deserialize<LedisString>(options),
            LedisValueType.List => root.Deserialize<LedisList>(options),
            LedisValueType.Set => root.Deserialize<LedisSet>(options),
            _ => throw new NotSupportedException($"Unknown LedisType: {type}")
        };
    }

    public override void Write(Utf8JsonWriter writer, LedisValue value, JsonSerializerOptions options)
    {
        if (value is LedisString stringValue)
        {
            JsonSerializer.Serialize(writer, stringValue, options);
        }
        else if (value is LedisList listValue)
        {
            JsonSerializer.Serialize(writer, listValue, options);
        }
        else if (value is LedisSet setValue)
        {
            JsonSerializer.Serialize(writer, setValue, options);
        }
    }
}