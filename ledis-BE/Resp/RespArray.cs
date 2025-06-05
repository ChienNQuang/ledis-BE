namespace ledis_BE.Resp;

public class RespArray : RespValue
{
    public IEnumerable<RespValue?> Elements { get; }
    public RespArray(IEnumerable<RespValue?> elements) => Elements = elements;

    public override RespValueType Type => RespValueType.Array;

    public override object GetValue()
    {
        List<object?> list = [];
        foreach (var item in Elements)
        {
            list.Add(item?.GetValue() ?? RespNull.Instance);
        }

        return ValueWithType(list);
    }
}