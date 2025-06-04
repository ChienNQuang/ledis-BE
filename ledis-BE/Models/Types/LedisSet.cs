namespace ledis_BE.Models.Types;

public class LedisSet : LedisValue
{
    public override LedisValueType Type => LedisValueType.Set;
}