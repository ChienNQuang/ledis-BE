using ledis_BE.Models.String;

namespace ledis_BE.Models.Set;

public class LedisSet : LedisValue
{
    public ISetValue<IStringValue> SetValue { get; set; }

    public LedisSet()
    {
    }
    
    public LedisSet(IEnumerable<string> values)
    {
        SetValue = new HashSetSetValue<IStringValue>(ToStringValues(values));
    }

    public override LedisValueType Type => LedisValueType.Set;

    public int SAdd(IEnumerable<string> values)
    {
        return SetValue.SAdd(ToStringValues(values));
    }

    public bool SRem(string value)
    {
        IStringValue strVal = ToStringValues([value]).First();
        return SetValue.SRem(strVal);
    }

    public IEnumerable<IStringValue> SMembers()
    {
        return SetValue.SMembers();
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
