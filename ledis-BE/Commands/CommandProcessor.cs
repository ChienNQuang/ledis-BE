using System.Text;
using ledis_BE.Models;
using ledis_BE.Models.List;
using ledis_BE.Models.String;
using ledis_BE.Resp;

namespace ledis_BE.Commands;

public static class CommandProcessor
{
    public static Result Process(DataStore dataStore, string command, string[] arguments)
    {
        switch (command.ToUpper())
        {
            case "GET":
                return Get(dataStore, arguments);
            case "SET":
                return Set(dataStore, arguments);
            case "RPUSH":
                return RPush(dataStore, arguments);
            case "RPOP":
                return RPop(dataStore, arguments);
            case "LRANGE":
                return LRange(dataStore, arguments);
            default:
                return Result.Fail(Errors.UnknownCommand(command, arguments));
        }
    }

    private static Result Get(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length != 1)
        {
            return Result.Fail(Errors.WrongNumberOfArguments("get"));
        }

        byte[] key = Encoding.UTF8.GetBytes(arguments[0]);

        if (!dataStore.Data.TryGetValue(key, out LedisValue? value))
        {
            return Result.Success(null);
        }

        if (value is null)
        {
            return Result.Success(null);
        }

        if (value is not LedisString stringValue)
        {
            return Result.Fail(Errors.WrongType);
        }

        return Result.Success(new RespBulkString(stringValue.ToString()));
    }

    private static Result Set(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length != 2)
        {
            return Result.Fail(Errors.WrongNumberOfArguments("set"));
        }

        byte[] key = Encoding.UTF8.GetBytes(arguments[0]);
        string value = arguments[1];

        dataStore.Data.Remove(key);
        dataStore.Data.Add(key, new LedisString(value));

        return Result.Ok();
    }

    private static Result RPush(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length < 2)
        {
            return Result.Fail(Errors.WrongNumberOfArguments("rpush"));
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
            return Result.Fail(Errors.WrongType);
        }

        var added = list.RPush(values);

        return Result.Success(new RespInteger(added));
    }

    private static Result RPop(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length != 1)
        {
            return Result.Fail(Errors.WrongNumberOfArguments("rpop"));
        }

        byte[] key = Encoding.UTF8.GetBytes(arguments[0]);

        if (!dataStore.Data.TryGetValue(key, out LedisValue? value))
        {
            return Result.Success(null);
        }

        if (value is not LedisList list)
        {
            return Result.Fail(Errors.WrongType);
        }

        IStringValue? popValue = list.RPop();

        return Result.Success(new RespBulkString(popValue?.AsString()));
    }
    
    private static Result LRange(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length != 3)
        {
            return Result.Fail(Errors.WrongNumberOfArguments("lrange"));
        }

        byte[] key = Encoding.UTF8.GetBytes(arguments[0]);
        
        if (!dataStore.Data.TryGetValue(key, out LedisValue? value))
        {
            return Result.Success(null);
        }

        if (value is not LedisList list)
        {
            return Result.Fail(Errors.WrongType);
        }

        var count = list.LLen();
        
        string startStr = arguments[1];
        string stopStr = arguments[2];

        if (!int.TryParse(startStr, out var start) || start < 0 || start >= count)
        {
            return Result.Fail(Errors.NotIntegerOrOutOfRange);
        }
        
        if (!int.TryParse(stopStr, out var stop) || stop < 0 || stop >= count)
        {
            return Result.Fail(Errors.NotIntegerOrOutOfRange);
        }

        if (start > stop)
        {
            // return empty array
            return Result.Success(null);
        }

        IEnumerable<IStringValue> range = list.LRange(start, stop);
        var resultElements = range.Select(x => new RespBulkString(x.AsString()));

        return Result.Success(new RespArray(resultElements));
    }
}