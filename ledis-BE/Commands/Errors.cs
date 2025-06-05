namespace ledis_BE.Commands;

public class Errors
{
    public static string WrongNumberOfArguments(string cmdName) => $"wrong number of arguments for '{cmdName}' command";
    public static string WrongType => "Operation against a key holding the wrong kind of value";
    public static string UnknownCommand(string cmdName, string[] arguments)
    {
        string argsStr = string.Join(" ", arguments.Select(x => $"'{x}'"));
        return $"unknown command '{cmdName}', with args beginning with: {argsStr}";
    }
    public static string NotIntegerOrOutOfRange => "value is not an integer or out of range";
}