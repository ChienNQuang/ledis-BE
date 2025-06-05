namespace ledis_BE.Resp;

public class RespMap : RespValue
{
    public List<(RespValue Key, RespValue Value)> Pairs { get; }
    public RespMap(List<(RespValue Key, RespValue Value)> pairs) => Pairs = pairs;

    public override RespValueType Type => RespValueType.Map;

    public override object GetValue()
    {
        // Represent as an array of [key, value] pairs for non-string keys
        List<object> result = [];
        foreach (var (key, val) in Pairs)
        {
            result.Add(new List<object?> {
                key.GetValue(),
                val.GetValue(),
            });
        }

        return ValueWithType(result);
    }
}