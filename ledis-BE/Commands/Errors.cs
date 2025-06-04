namespace ledis_BE.Commands;

public class Errors
{
    public static string WrongNumberOfArguments(string cmdName) => $"wrong number of arguments for '{cmdName}' command";
    public static string WrongType => "Operation against a key holding the wrong kind of value";
}