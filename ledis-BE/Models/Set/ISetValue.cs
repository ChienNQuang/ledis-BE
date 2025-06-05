using ledis_BE.Models.String;

namespace ledis_BE.Models.Set;

public interface ISetValue<T> where T : IStringValue
{
    int SAdd(IEnumerable<T> values);
}