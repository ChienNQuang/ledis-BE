using System.Text;
using ledis_BE.Models;
using ledis_BE.Models.String;

namespace ledis_BE.Commands;

public static class CommandProcessor
{
    private static readonly string[] BaseCommands = ["GET", "SET"];

    public static string Process(DataStore dataStore, string command, string[] arguments)
    {
        string uppercaseCommand = command.ToUpper();
        if (BaseCommands.Any(c => c.Equals(uppercaseCommand)))
        {
            switch (uppercaseCommand)
            {
                case "GET":
                    return Get(dataStore, arguments);
                case "SET":
                    return Set(dataStore, arguments);
            }
        }

        return string.Empty;
    }

    private static string Get(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length != 1)
        {
            return "ERROR: wrong number of arguments for 'get' command";
        }

        byte[] key = Encoding.UTF8.GetBytes(arguments[0]);

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

    private static string Set(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length != 2)
        {
            return "ERROR: wrong number of arguments for 'set' command";
        }

        byte[] key = Encoding.UTF8.GetBytes(arguments[0]);
        string value = arguments[1];

        dataStore.Data.Remove(key);
        dataStore.Data.Add(key, new LedisString(value));

        return "OK";
    }
}