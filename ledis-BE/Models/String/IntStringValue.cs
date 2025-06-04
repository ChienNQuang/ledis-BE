namespace ledis_BE.Models.String;

public struct IntStringValue : IStringValue
{
    private readonly long _value;

    public IntStringValue(long value)
    {
        _value = value;
    }

    public string AsString()
    {
        return _value.ToString();
    }
}