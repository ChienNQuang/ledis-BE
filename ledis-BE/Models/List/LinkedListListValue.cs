using ledis_BE.DataStructures;
using ledis_BE.Models.String;

namespace ledis_BE.Models.List;

public class LinkedListListValue<T> : IListValue<T> where T : IStringValue
{
    public DoublyLinkedList<T> List { get; set; }

    public LinkedListListValue()
    {
    }

    public LinkedListListValue(IEnumerable<T> values)
    {
        List = new DoublyLinkedList<T>();
        RPush(values);
    }
    
    public List<T> AsList()
    {
        return List.Values().ToList();
    }

    public ListValueEncoding Encoding => ListValueEncoding.LinkedList;

    public void RPush(IEnumerable<T> values)
    {
        foreach (T value in values)
        {
            List.AddLast(value);
        }
    }

    public T? RPop()
    {
        return List.RemoveLast();
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