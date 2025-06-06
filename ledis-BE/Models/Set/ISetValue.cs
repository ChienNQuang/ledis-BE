using ledis_BE.Models.String;

namespace ledis_BE.Models.Set;

public interface ISetValue<T> where T : IStringValue
{
    SetValueEncoding Encoding { get; }
    int SAdd(IEnumerable<T> values);
    bool SRem(T value);
    IEnumerable<T> SMembers();
}

public enum SetValueEncoding
{
    Hashtable,
}