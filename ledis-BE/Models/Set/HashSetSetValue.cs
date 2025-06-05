using ledis_BE.Comparers;
using ledis_BE.Models.String;

namespace ledis_BE.Models.Set;

public class HashSetSetValue<T> : ISetValue<T> where T : IStringValue
{
    private readonly HashSet<T> _set;

    public HashSetSetValue(IEnumerable<T> values)
    {
        _set = new HashSet<T>(new StringValueComparer<T>());
        foreach (T value in values)
        {
            _set.Add(value);
        }
    }

    public int SAdd(IEnumerable<T> values)
    {
        int count = 0;
        foreach (T value in values)
        {
            if (_set.Add(value)) count++;
        }

        return count;
    }

    public bool SRem(T value)
    {
        return _set.Remove(value);
    }
}