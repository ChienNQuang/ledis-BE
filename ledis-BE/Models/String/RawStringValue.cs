using System.Text;

namespace ledis_BE.Models.String;

public class RawStringValue : IStringValue
{
    private readonly byte[] _bytes;

    public RawStringValue(string value)
    {
        _bytes = Encoding.UTF8.GetBytes(value);
    }

    public string AsString()
    {
        return Encoding.UTF8.GetString(_bytes);
    }
}