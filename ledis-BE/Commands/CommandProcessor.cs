using System.Text;
using ledis_BE.Models;
using ledis_BE.Models.List;
using ledis_BE.Models.String;

namespace ledis_BE.Commands;

public static class CommandProcessor
{
    public static Result<string> Process(DataStore dataStore, string command, string[] arguments)
    {
        switch (command.ToUpper())
        {
            case "GET":
                return Get(dataStore, arguments);
            case "SET":
                return Set(dataStore, arguments);
            case "RPUSH":
                return RPush(dataStore, arguments);
            default:
                return Result<string>.Fail(Errors.UnknownCommand(command, arguments));
        }
    }

    private static Result<string> Get(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length != 1)
        {
            return Result<string>.Fail(Errors.WrongNumberOfArguments("get"));
        }

        byte[] key = Encoding.UTF8.GetBytes(arguments[0]);

        if (!dataStore.Data.TryGetValue(key, out LedisValue? value))
        {
            return Result<string>.Success(null);
        }

        if (value is null)
        {
            return Result<string>.Success(null);
        }

        if (value is not LedisString stringValue)
        {
            return Result<string>.Fail(Errors.WrongType);
        }

        return Result<string>.Success($"\"{stringValue}\"");
    }

    private static Result<string> Set(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length != 2)
        {
            return Result<string>.Fail(Errors.WrongNumberOfArguments("set"));
        }

        byte[] key = Encoding.UTF8.GetBytes(arguments[0]);
        string value = arguments[1];

        dataStore.Data.Remove(key);
        dataStore.Data.Add(key, new LedisString(value));

        return Result<string>.Ok();
    }

    private static Result<string> RPush(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length < 2)
        {
            return Result<string>.Fail(Errors.WrongNumberOfArguments("rpush"));
        }

        byte[] key = Encoding.UTF8.GetBytes(arguments[0]);
        string[] values = arguments[1..];
        
        if (!dataStore.Data.TryGetValue(key, out LedisValue? value))
        {
            value = new LedisList([]);
            dataStore.Data.TryAdd(key, value);
        }

        if (value is not LedisList list)
        {
            return Result<string>.Fail(Errors.WrongType);
        }

        var added = list.RPush(values);

        return Result<string>.Success(added.ToString());
    }
    
    
}