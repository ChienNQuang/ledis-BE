using System.Text;

namespace ledis_BE.Models.String;

public class RawStringValue : IStringValue
{
    public byte[] Bytes { get; set; }

    public RawStringValue(string value)
    {
        Bytes = Encoding.UTF8.GetBytes(value);
    }

    public string AsString()
    {
        return Encoding.UTF8.GetString(Bytes);
    }
}