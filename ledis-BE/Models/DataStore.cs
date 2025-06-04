using ledis_BE.Comparers;

namespace ledis_BE.Models;

public class DataStore
{
    /// <summary>
    /// Key is stored as a byte array.
    /// </summary>
    public Dictionary<byte[], LedisValue?> Data { get; set; } = new(new ByteArrayComparer());
}