using ledis_BE.DataStructures;
using ledis_BE.Models.String;

namespace ledis_BE.Models.List;

public class LinkedListListValue<T> : IListValue<T> where T : IStringValue
{
    private readonly DoublyLinkedList<T> _list;

    public LinkedListListValue(IEnumerable<T> values)
    {
        _list = new DoublyLinkedList<T>();
        foreach (T value in values)
        {
            _list.AddLast(value);
        }
    }
    
    public List<T> AsList()
    {
        return _list.Values().ToList();
    }
}