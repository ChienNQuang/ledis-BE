namespace ledis_BE.Models.String;

public struct IntStringValue : IStringValue
{
    public long Value { get; set; }

    public IntStringValue()
    {
    }

    public IntStringValue(long value)
    {
        Value = value;
    }

    public string AsString()
    {
        return Value.ToString();
    }

    public StringValueEncoding Encoding => StringValueEncoding.Int;
}