namespace ledis_BE.Models.DataStructures;

public class LedisList : LedisValue
{
    public override LedisValueType Type => LedisValueType.List;
}