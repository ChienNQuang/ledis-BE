using System.Text.Json;
using System.Text.Json.Serialization;
using ledis_BE.DataStructures;

namespace ledis_BE.Converters;

public class DoublyLinkedListConverter<T> : JsonConverter<DoublyLinkedList<T>>
{
    public override DoublyLinkedList<T>? Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        JsonElement root = doc.RootElement;

        List<SerializableDoublyLinkedListNode<T>>? serializableList =
            root.Deserialize<List<SerializableDoublyLinkedListNode<T>>>(options);
        if (serializableList is not null)
        {
            DoublyLinkedList<T> linkedList = new DoublyLinkedList<T>();
            foreach (SerializableDoublyLinkedListNode<T> node in serializableList)
            {
                linkedList.AddLast(node.Value);
            }

            return linkedList;
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, DoublyLinkedList<T> value, JsonSerializerOptions options)
    {
        List<T> list = value.Values().ToList();
        List<SerializableDoublyLinkedListNode<T>> serializableList = list
            .Select(item => new SerializableDoublyLinkedListNode<T>
            {
                Value = item,
            })
            .ToList();

        JsonSerializer.Serialize(writer, serializableList, options);
    }
}

public class SerializableDoublyLinkedListNode<T>
{
    public T Value { get; set; }
}