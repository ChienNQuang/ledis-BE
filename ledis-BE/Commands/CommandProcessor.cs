using System.Text;
using ledis_BE.Models;
using ledis_BE.Models.List;
using ledis_BE.Models.Set;
using ledis_BE.Models.String;
using ledis_BE.Resp;

namespace ledis_BE.Commands;

public static class CommandProcessor
{
    public static RespValue Process(DataStore dataStore, string command, string[] arguments)
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
            case "SADD":
                return SAdd(dataStore, arguments);
            case "SREM":
                return SRem(dataStore, arguments);
            case "SMEMBERS":
                return SMembers(dataStore, arguments);
            default:
                return new RespError(Errors.UnknownCommand(command, arguments));
        }
    }

    private static RespValue Get(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length != 1)
        {
            return new RespError(Errors.WrongNumberOfArguments("get"));
        }

        byte[] key = Encoding.UTF8.GetBytes(arguments[0]);

        if (!dataStore.Data.TryGetValue(key, out LedisValue? value))
        {
            return new RespNull();
        }

        if (value is null)
        {
            return new RespNull();
        }

        if (value is not LedisString stringValue)
        {
            return new RespError(Errors.WrongType);
        }

        return new RespBulkString(stringValue.ToString());
    }

    private static RespValue Set(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length != 2)
        {
            return new RespError(Errors.WrongNumberOfArguments("set"));
        }

        byte[] key = Encoding.UTF8.GetBytes(arguments[0]);
        string value = arguments[1];

        dataStore.Data.Remove(key);
        dataStore.Data.Add(key, new LedisString(value));

        return RespSimpleString.Ok();
    }

    private static RespValue RPush(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length < 2)
        {
            return new RespError(Errors.WrongNumberOfArguments("rpush"));
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
            return new RespError(Errors.WrongType);
        }

        var added = list.RPush(values);

        return new RespInteger(added);
    }

    private static RespValue RPop(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length != 1)
        {
            return new RespError(Errors.WrongNumberOfArguments("rpop"));
        }

        byte[] key = Encoding.UTF8.GetBytes(arguments[0]);

        if (!dataStore.Data.TryGetValue(key, out LedisValue? value))
        {
            return new RespNull();
        }

        if (value is not LedisList list)
        {
            return new RespError(Errors.WrongType);
        }

        IStringValue? popValue = list.RPop();

        return new RespBulkString(popValue?.AsString());
    }

    private static RespValue LRange(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length != 3)
        {
            return new RespError(Errors.WrongNumberOfArguments("lrange"));
        }

        byte[] key = Encoding.UTF8.GetBytes(arguments[0]);

        if (!dataStore.Data.TryGetValue(key, out LedisValue? value))
        {
            return new RespNull();
        }

        if (value is not LedisList list)
        {
            return new RespError(Errors.WrongType);
        }

        var count = list.LLen();

        string startStr = arguments[1];
        string stopStr = arguments[2];

        if (!int.TryParse(startStr, out var start) || start < 0 || start >= count)
        {
            return new RespError(Errors.NotIntegerOrOutOfRange);
        }

        if (!int.TryParse(stopStr, out var stop) || stop < 0 || stop >= count)
        {
            return new RespError(Errors.NotIntegerOrOutOfRange);
        }

        if (start > stop)
        {
            // return empty array
            return new RespArray([]);
        }

        IEnumerable<IStringValue> range = list.LRange(start, stop);
        var resultElements = range.Select(x => new RespBulkString(x.AsString()));

        return new RespArray(resultElements);
    }

    private static RespValue SAdd(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length < 2)
        {
            return new RespError(Errors.WrongNumberOfArguments("sadd"));
        }

        byte[] key = Encoding.UTF8.GetBytes(arguments[0]);
        string[] values = arguments[1..];

        if (!dataStore.Data.TryGetValue(key, out LedisValue? value))
        {
            value = new LedisSet([]);
            dataStore.Data.TryAdd(key, value);
        }

        if (value is not LedisSet set)
        {
            return new RespError(Errors.WrongType);
        }

        var added = set.SAdd(values);

        return new RespInteger(added);
    }
    
    private static RespValue SRem(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length != 2)
        {
            return new RespError(Errors.WrongNumberOfArguments("srem"));
        }

        byte[] key = Encoding.UTF8.GetBytes(arguments[0]);
        string valueToRemove = arguments[1];

        if (!dataStore.Data.TryGetValue(key, out LedisValue? value))
        {
            return new RespBoolean(false);
        }

        if (value is not LedisSet set)
        {
            return new RespError(Errors.WrongType);
        }

        bool res = set.SRem(valueToRemove);

        return new RespBoolean(res);
    }
    
    private static RespValue SMembers(DataStore dataStore, string[] arguments)
    {
        if (arguments.Length != 1)
        {
            return new RespError(Errors.WrongNumberOfArguments("smembers"));
        }

        byte[] key = Encoding.UTF8.GetBytes(arguments[0]);

        if (!dataStore.Data.TryGetValue(key, out LedisValue? value))
        {
            return new RespArray([]);
        }

        if (value is not LedisSet set)
        {
            return new RespError(Errors.WrongType);
        }

        IEnumerable<IStringValue> members = set.SMembers();
        IEnumerable<RespBulkString> resultElements = members.Select(x => new RespBulkString(x.AsString()));

        return new RespArray(resultElements);
    }
}