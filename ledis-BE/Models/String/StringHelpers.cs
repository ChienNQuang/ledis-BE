namespace ledis_BE.Models.String;

public static class StringHelpers
{
    public static (IStringValue, LedisStringEncoding) GetValueAndEncoding(string value)
    {
        // if the string can be parsed to a long, then stored as int encoding
        if (long.TryParse(value, out long result))
        {
            return (new IntStringValue(result), LedisStringEncoding.Int);
        }

        return (new RawStringValue(value), LedisStringEncoding.Raw);
    }
}