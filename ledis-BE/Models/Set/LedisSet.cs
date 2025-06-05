using ledis_BE.Models.String;

namespace ledis_BE.Models.Set;

public class LedisSet : LedisValue
{
    private readonly ISetValue<IStringValue> _setValue;
    private readonly LedisSetEncoding _encoding;

    public LedisSet(IEnumerable<string> values)
    {
        _setValue = new HashSetSetValue<IStringValue>(ToStringValues(values));
        _encoding = LedisSetEncoding.Hashtable;
    }

    public override LedisValueType Type => LedisValueType.Set;

    public int SAdd(IEnumerable<string> values)
    {
        return _setValue.SAdd(ToStringValues(values));
    }

    public bool SRem(string value)
    {
        IStringValue strVal = ToStringValues([value]).First();
        return _setValue.SRem(strVal);
    }

    private static IEnumerable<IStringValue> ToStringValues(IEnumerable<string> values)
    {
        return values.Select(v =>
        {
            (IStringValue value, _) = StringHelpers.GetValueAndEncoding(v);
            return value;
        });
    }
}

public enum LedisSetEncoding
{
    Hashtable,
}