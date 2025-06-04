namespace ledis_BE.Models.String;

public class LedisString : LedisValue
{
    private readonly IStringValue _value;
    private readonly LedisStringEncoding _encoding;

    public LedisString(string value)
    {
        (_value, _encoding) = StringHelpers.GetValueAndEncoding(value);
    }

    public override LedisValueType Type => LedisValueType.String;

    public override string ToString()
    {
        return _value.AsString();
    }
}

public enum LedisStringEncoding
{
    Raw,
    Int,
}