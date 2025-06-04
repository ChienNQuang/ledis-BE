namespace ledis_BE.Models.String;

public class LedisString : LedisValue
{
    private readonly IStringValue _value;
    private readonly LedisStringEncoding _encoding;

    public LedisString(string value)
    {
        // if the string can be parsed to a long, then stored as int encoding
        if (long.TryParse(value, out long result))
        {
            _value = new IntStringValue(result);
            _encoding = LedisStringEncoding.Int;
        }
        else
        {
            _value = new RawStringValue(value);
            _encoding = LedisStringEncoding.Raw;
        }
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