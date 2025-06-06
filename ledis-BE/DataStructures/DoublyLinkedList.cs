namespace ledis_BE.DataStructures;

public class DoublyLinkedList<T>
{
    public DoublyLinkedListNode<T>? Head { get; set; }
    public DoublyLinkedListNode<T>? Tail { get; set; }

    public void AddLast(T value)
    {
        var newNode = new DoublyLinkedListNode<T>(value);

        if (Head is null)
        {
            Head = Tail = newNode;
        }
        else
        {
            Tail!.Next = newNode;
            newNode.Prev = Tail;
            Tail = newNode;
        }
    }

    public T? RemoveLast()
    {
        if (Tail is null) return default;

        T value = Tail.Value;

        Tail = Tail.Prev;
        if (Tail is not null) Tail.Next = null;
        else Head = null;

        return value;
    }

    public IEnumerable<T> Values()
    {
        DoublyLinkedListNode<T>? curr = Head;
        while (curr is not null)
        {
            yield return curr.Value;
            curr = curr.Next;
        }
    }
}

public class DoublyLinkedListNode<T>
{
    public T Value { get; set; }
    public DoublyLinkedListNode<T>? Prev { get; set; }
    public DoublyLinkedListNode<T>? Next { get; set; }

    public DoublyLinkedListNode(T value)
    {
        Value = value;
    }
}
