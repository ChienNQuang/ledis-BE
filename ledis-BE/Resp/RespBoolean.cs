namespace ledis_BE.Resp;

public class RespBoolean : RespValue
{
    public bool Value { get; }
    public RespBoolean(bool value) => Value = value;

    public override RespValueType Type => RespValueType.Boolean;
    public override object GetValue() => ValueWithType(Value);
}