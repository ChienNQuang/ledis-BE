using ledis_BE.Models.String;

namespace ledis_BE.Models.List;

public class LedisList : LedisValue
{
    public IListValue<IStringValue> ListValue { get; set; }

    public LedisList()
    {
    }
    
    public LedisList(IEnumerable<string> values)
    {
        ListValue = new LinkedListListValue<IStringValue>(ToStringValues(values));
    }
    
    public override LedisValueType Type => LedisValueType.List;

    public int RPush(IEnumerable<string> values)
    {
        return ListValue.RPush(ToStringValues(values));
    }
    
    public IStringValue? RPop()
    {
        return ListValue.RPop();
    }

    public IEnumerable<IStringValue> LRange(int start, int stop)
    {
        return ListValue.LRange(start, stop);
    }

    public int LLen()
    {
        return ListValue.LLen();
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
