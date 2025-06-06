using System.Text.Json;
using ledis_BE.Converters;
using ledis_BE.Models.String;

namespace ledis_BE.Models;

public static class LedisJsonOptions
{
    public static readonly JsonSerializerOptions Default = new()
    {
        WriteIndented = true,
        Converters =
        {
            new LedisValueConverter(),
            new StringValueConverter(),
            new ListValueConverter(),
            new SetValueConverter(),
            new DoublyLinkedListConverter<IStringValue>(),
        }
    };
}
