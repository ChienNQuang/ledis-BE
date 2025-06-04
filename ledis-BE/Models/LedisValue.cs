namespace ledis_BE.Models;

public abstract class LedisValue
{
    public abstract LedisValueType Type { get; }
    public long Expiration { get; set; } = -1; // Default to no expiration
}

public enum LedisValueType
{
    String,
    List,
    Set,
}