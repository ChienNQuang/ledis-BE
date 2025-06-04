namespace ledis_BE.Models.DataStructures;

public class LedisString : LedisValue
{
    public byte[] Value { get; set; }

    public LedisString(byte[] value)
    {
        Value = value;
    }

    public override LedisValueType Type => LedisValueType.String;
}