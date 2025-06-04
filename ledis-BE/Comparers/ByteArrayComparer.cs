namespace ledis_BE.Comparers;

public class ByteArrayComparer : IEqualityComparer<byte[]>
{
    public bool Equals(byte[]? x, byte[]? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null || y is null) return false;
        if (x.Length != y.Length) return false;

        for (int i = 0; i < x.Length; i++)
            if (x[i] != y[i])
                return false;

        return true;
    }

    public int GetHashCode(byte[] obj)
    {
        if (obj is null) return 0;
        unchecked
        {
            int hash = 17;
            foreach (byte b in obj)
                hash = hash * 31 + b;
            return hash;
        }
    }
}
