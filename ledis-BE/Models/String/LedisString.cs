namespace ledis_BE.Models.String;

public class LedisString : LedisValue
{
    public IStringValue Value { get; set; }

    public LedisString(string value)
    {
        (Value, _) = StringHelpers.GetValueAndEncoding(value);
    }

    public override LedisValueType Type => LedisValueType.String;

    public override string ToString()
    {
        return Value.AsString();
    }
}
