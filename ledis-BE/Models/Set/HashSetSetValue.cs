using ledis_BE.Comparers;
using ledis_BE.Models.String;

namespace ledis_BE.Models.Set;

public class HashSetSetValue<T> : ISetValue<T> where T : IStringValue
{
    public HashSet<T> Set { get; set; }

    public HashSetSetValue()
    {
    }
    
    public HashSetSetValue(IEnumerable<T> values)
    {
        Set = new HashSet<T>(new StringValueComparer<T>());
        foreach (T value in values)
        {
            Set.Add(value);
        }
    }

    public SetValueEncoding Encoding => SetValueEncoding.Hashtable;

    public int SAdd(IEnumerable<T> values)
    {
        int count = 0;
        foreach (T value in values)
        {
            if (Set.Add(value)) count++;
        }

        return count;
    }

    public bool SRem(T value)
    {
        return Set.Remove(value);
    }

    public IEnumerable<T> SMembers()
    {
        return Set;
    }
}