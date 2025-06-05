namespace ledis_BE.Models;

public abstract class LedisValue
{
    public abstract LedisValueType Type { get; }
}

public enum LedisValueType
{
    String,
    List,
    Set,
}