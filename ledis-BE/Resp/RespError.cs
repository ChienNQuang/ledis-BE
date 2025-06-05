namespace ledis_BE.Resp;

public class RespError : RespValue
{
    public string Message { get; }
    public RespError(string message) => Message = message;

    public override RespValueType Type => RespValueType.Error;

    public override object GetValue() => ValueWithType(Message);
}