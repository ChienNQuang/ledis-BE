using ledis_BE.Models;

namespace ledis_BE.Commands;

public class SerializableDataStore
{
    public Dictionary<string, LedisValue?> Data { get; set; } = new();
    public Dictionary<string, long> Expires { get; set; } = new();
}