namespace ledis_BE.Resp;

public class RespNull : RespValue
{
    public static readonly object Instance = null!;
    public override RespValueType Type => RespValueType.Null;

    public override object GetValue() => ValueWithType(Instance);
}