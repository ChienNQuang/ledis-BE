using ledis_BE.Models.String;

namespace ledis_BE.Comparers;

public class StringValueComparer<T> : IEqualityComparer<T> where T : IStringValue
{
    public bool Equals(T? x, T? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null || y is null) return false;
        return x.AsString().Equals(y.AsString());
    }

    public int GetHashCode(T obj)
    {
        return obj.AsString().GetHashCode();
    }
}