namespace ledis_BE.Resp;

public abstract class RespValue
{
    public abstract RespValueType Type { get; }

    public abstract object GetValue();

    protected Dictionary<string, object?> ValueWithType(object? value)
    {
        return new Dictionary<string, object?> {
            { "type", Type },
            { "value", value },
        };
    }
}

public enum RespValueType
{
    SimpleString,
    Error,
    Integer,
    BulkString,
    Array,
    Null,
    Boolean,
    Map,
}