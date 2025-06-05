using ledis_BE.DataStructures;
using ledis_BE.Models.String;

namespace ledis_BE.Models.List;

public class LinkedListListValue<T> : IListValue<T> where T : IStringValue
{
    private readonly DoublyLinkedList<T> _list;

    public LinkedListListValue(IEnumerable<T> values)
    {
        _list = new DoublyLinkedList<T>();
        RPush(values);
    }
    
    public List<T> AsList()
    {
        return _list.Values().ToList();
    }

    public int RPush(IEnumerable<T> values)
    {
        int count = 0;
        foreach (T value in values)
        {
            _list.AddLast(value);
            count++;
        }

        return count;
    }

    public T? RPop()
    {
        return _list.RemoveLast();
    }

    public IEnumerable<T> LRange(int start, int stop)
    {
        List<T> list = AsList();
        // get until the list ends instead of throwing exceptions
        for (int i = start; i <= stop && i < list.Count; i++)
        {
            yield return list[i];
        }
    }

    public int LLen()
    {
        return AsList().Count;
    }
}