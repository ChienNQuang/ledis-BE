namespace ledis_BE.Resp;

public class RespSimpleString : RespValue
{
    public string Value { get; }
    public RespSimpleString(string value) => Value = value;

    public override RespValueType Type => RespValueType.SimpleString;
    public override object GetValue() => ValueWithType(Value);
}