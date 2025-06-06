namespace ledis_BE.Models.String;

/// <summary>
/// The abstracted value of a string type, inherited children are implementation, in Redis there are
/// `bit`, `raw` and `embstr`
/// </summary>
public interface IStringValue
{
    string AsString();
    StringValueEncoding Encoding { get; }
}

public enum StringValueEncoding
{
    Raw,
    Int,
}
