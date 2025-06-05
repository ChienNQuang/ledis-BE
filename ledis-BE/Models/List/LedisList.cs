using ledis_BE.Models.String;

namespace ledis_BE.Models.List;

public class LedisList : LedisValue
{
    private readonly IListValue<IStringValue> _values;
    private readonly LedisListEncoding _encoding;

    public LedisList(IEnumerable<string> values)
    {
        _values = new LinkedListListValue<IStringValue>(ToStringValues(values));
        _encoding = LedisListEncoding.LinkedList;
    }
    
    public override LedisValueType Type => LedisValueType.List;

    public int RPush(IEnumerable<string> values)
    {
        return _values.RPush(ToStringValues(values));
    }
    
    public IStringValue? RPop()
    {
        return _values.RPop();
    }

    public IEnumerable<IStringValue> LRange(int start, int stop)
    {
        return _values.LRange(start, stop);
    }

    public int LLen()
    {
        return _values.LLen();
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

public enum LedisListEncoding
{
    LinkedList,
}