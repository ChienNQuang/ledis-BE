namespace ledis_BE.Resp;

public class RespBulkString : RespValue
{
    public string? Value { get; }
    public RespBulkString(string? value) => Value = value;

    public override RespValueType Type => RespValueType.BulkString;
    public override object GetValue() => ValueWithType(Value ?? RespNull.Instance);
}