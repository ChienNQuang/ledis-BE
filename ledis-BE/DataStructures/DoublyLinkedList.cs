namespace ledis_BE.DataStructures;

public class DoublyLinkedList<T>
{
    private DoublyLinkedListNode<T>? head;
    private DoublyLinkedListNode<T>? tail;

    public void AddLast(T value)
    {
        var newNode = new DoublyLinkedListNode<T>(value);

        if (head is null)
        {
            head = tail = newNode;
        }
        else
        {
            tail!.Next = newNode;
            newNode.Prev = tail;
            tail = newNode;
        }
    }

    public IEnumerable<T> Values()
    {
        DoublyLinkedListNode<T>? curr = head;
        while (curr is not null)
        {
            yield return curr.Value;
            curr = curr.Next;
        }
    }
}

public class DoublyLinkedListNode<T>
{
    public T Value;
    public DoublyLinkedListNode<T>? Prev;
    public DoublyLinkedListNode<T>? Next;

    public DoublyLinkedListNode(T value)
    {
        Value = value;
    }
}
