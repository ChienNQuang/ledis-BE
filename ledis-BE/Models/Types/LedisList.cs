namespace ledis_BE.Models.Types;

public class LedisList : LedisValue
{
    public override LedisValueType Type => LedisValueType.List;
}