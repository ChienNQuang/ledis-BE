namespace ledis_BE.Resp;

public class RespInteger : RespValue
{
    public long Value { get; }
    public RespInteger(long value) => Value = value;

    public override RespValueType Type => RespValueType.Integer;
    public override object GetValue() => ValueWithType(Value);
}