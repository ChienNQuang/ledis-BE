namespace ledis_BE.Models.DataStructures;

public class LedisString : LedisValue
{
    public override LedisValueType Type => LedisValueType.String;
}