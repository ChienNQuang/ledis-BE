using ledis_BE.Models.String;

namespace ledis_BE.Models.List;

public class LedisList : LedisValue
{
    private readonly IListValue<IStringValue> _values;
    private readonly LedisListEncoding _encoding;

    public LedisList(IEnumerable<string> values)
    {
        _values = new LinkedListListValue<IStringValue>(values.Select(v =>
        {
            (IStringValue value, _) = StringHelpers.GetValueAndEncoding(v);
            return value;
        }));
        _encoding = LedisListEncoding.LinkedList;
    }
    
    public override LedisValueType Type => LedisValueType.List;
}

public enum LedisListEncoding
{
    LinkedList,
}