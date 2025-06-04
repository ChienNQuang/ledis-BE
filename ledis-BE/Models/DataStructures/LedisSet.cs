namespace ledis_BE.Models.DataStructures;

public class LedisSet : LedisValue
{
    public override LedisValueType Type => LedisValueType.Set;
}