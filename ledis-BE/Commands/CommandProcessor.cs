using ledis_BE.Models;
using ledis_BE.Models.DataStructures;

namespace ledis_BE.Commands;

public static class CommandProcessor
{
    private static readonly string[] BaseCommands = ["GET", "SET"];

    public static string Process(DataStore dataStore, string command, string[] arguments)
    {
        string uppercaseCommand = command.ToUpper();
        byte[][] byteArguments = arguments.Select(Convert.FromBase64String).ToArray();
        if (BaseCommands.Any(c => c.Equals(uppercaseCommand)))
        {
            switch (uppercaseCommand)
            {
                case "GET":
                    return Get(dataStore, byteArguments);
                case "SET":
                    return Set(dataStore, byteArguments);
            }
        }

        return string.Empty;
    }

    private static string Get(DataStore dataStore, byte[][] arguments)
    {
        if (arguments.Length != 1)
        {
            return "ERROR: wrong number of arguments for 'get' command";
        }

        byte[] key = arguments[0];

        if (!dataStore.Data.TryGetValue(key, out LedisValue? value))
        {
            return "(nil)";
        }

        if (value is null)
        {
            return "(nil)";
        }

        if (value is not LedisString stringValue)
        {
            return "ERROR: WRONGTYPE Operation against a key holding the wrong kind of value";
        }

        return $"\"{stringValue}\"";
    }

    private static string Set(DataStore dataStore, byte[][] arguments)
    {
        if (arguments.Length != 2)
        {
            return "ERROR: wrong number of arguments for 'set' command";
        }

        byte[] key = arguments[0];
        byte[] value = arguments[1];

        dataStore.Data.Remove(key);
        dataStore.Data.Add(key, new LedisString(value));

        return "OK";
    }
}