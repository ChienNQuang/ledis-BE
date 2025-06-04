using ledis_BE.Models.String;

namespace ledis_BE.Models.List;

/// <summary>
/// The abstracted value of a string type, inherited children are implementation, in Redis there are
/// `linkedlist`, `ziplist`, `listpack` and `quicklist`
/// </summary>
public interface IListValue<T> where T : IStringValue
{
    List<T> AsList();
    int RPush(IEnumerable<T> values);
}