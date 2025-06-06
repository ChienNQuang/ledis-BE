namespace ledis_BE.Models.String;

public class LedisString : LedisValue
{
    public IStringValue Value { get; set; }
    public LedisStringEncoding Encoding { get; set; }

    public LedisString(string value)
    {
        (Value, Encoding) = StringHelpers.GetValueAndEncoding(value);
    }

    public override LedisValueType Type => LedisValueType.String;

    public override string ToString()
    {
        return Value.AsString();
    }
}

public enum LedisStringEncoding
{
    Raw,
    Int,
}