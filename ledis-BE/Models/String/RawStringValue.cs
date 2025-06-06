namespace ledis_BE.Models.String;

public class RawStringValue : IStringValue
{
    public byte[] Bytes { get; set; }

    public RawStringValue(string value)
    {
        Bytes = System.Text.Encoding.UTF8.GetBytes(value);
    }

    public string AsString()
    {
        return System.Text.Encoding.UTF8.GetString(Bytes);
    }

    public StringValueEncoding Encoding => StringValueEncoding.Raw;
}