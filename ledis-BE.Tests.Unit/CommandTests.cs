using System.Text;
using ledis_BE.Commands;
using ledis_BE.Models;
using ledis_BE.Models.List;
using ledis_BE.Models.String;
using ledis_BE.Resp;
using Shouldly;

namespace ledis_BE.Tests.Unit;

public class CommandTests
{
    private DataStore dataStore = new();
    
    [Fact]
    public void ShouldReturnError_WhenCommandNotFound()
    {
        // Act
        RespValue res = CommandProcessor.Process(dataStore, "abcxyz", []);
        
        // Assert
        Dictionary<string, object?>? dict = res.GetValue() as Dictionary<string, object?>;
        dict.ShouldNotBeNull();
        dict.ShouldContain(p => p.Key == "type" && p.Value != null && p.Value.Equals(RespValueType.Error));
        dict.ShouldContain(p => p.Key == "value" && p.Value != null && p.Value.ToString()!.StartsWith($"unknown command 'abcxyz', with args beginning with: "));
    }

    [Fact]
    public void GET_ShouldReturnNull_WhenValueDoesNotExist()
    {
        // Arrange
        string keyStr = "random key";
        
        // Act
        RespValue res = CommandProcessor.Process(dataStore, "GET", [keyStr]);
        
        // Assert
        Dictionary<string, object?>? dict = res.GetValue() as Dictionary<string, object?>;
        dict.ShouldNotBeNull();
        dict.ShouldContain(p => p.Key == "type" && p.Value != null && p.Value.Equals(RespValueType.Null));
        dict.ShouldContain(p => p.Key == "value" && p.Value == null);
    }
    
    [Fact]
    public void GET_ShouldReturnValue_WhenValueExists()
    {
        // Arrange
        string keyStr = "random key";
        string value = "random value";
        byte[] key = Encoding.UTF8.GetBytes(keyStr);
        dataStore.Data.Add(key, new LedisString(value));
        
        // Act
        RespValue res = CommandProcessor.Process(dataStore, "GET", [keyStr]);
        
        // Assert
        Dictionary<string, object?>? dict = res.GetValue() as Dictionary<string, object?>;
        dict.ShouldNotBeNull();
        dict.ShouldContain(p => p.Key == "type" && p.Value != null && p.Value.Equals(RespValueType.BulkString));
        dict.ShouldContain(p => p.Key == "value" && p.Value != null && p.Value.Equals(value));
    }
    
    [Fact]
    public void GET_ShouldThrowWrongTypeError_WhenValueIsNotString()
    {
        // Arrange
        string keyStr = "random key";
        string value = "random value";
        byte[] key = Encoding.UTF8.GetBytes(keyStr);
        dataStore.Data.Add(key, new LedisList([value]));
        
        // Act
        RespValue res = CommandProcessor.Process(dataStore, "GET", [keyStr]);
        
        // Assert
        Dictionary<string, object?>? dict = res.GetValue() as Dictionary<string, object?>;
        dict.ShouldNotBeNull();
        dict.ShouldContain(p => p.Key == "type" && p.Value != null && p.Value.Equals(RespValueType.Error));
        dict.ShouldContain(p => p.Key == "value" && p.Value != null && p.Value.Equals(Errors.WrongType));
    }
    
    [Fact]
    public void SET_ShouldReturnOK()
    {
        // Arrange
        string keyStr = "random key";
        string value = "random value";
        string newValue = "random value 2";
        byte[] key = Encoding.UTF8.GetBytes(keyStr);
        dataStore.Data.Add(key, new LedisList([value]));
        
        // Act
        RespValue res = CommandProcessor.Process(dataStore, "SET", [keyStr, newValue]);
        
        // Assert
        Dictionary<string, object?>? dict = res.GetValue() as Dictionary<string, object?>;
        dict.ShouldNotBeNull();
        dict.ShouldContain(p => p.Key == "type" && p.Value != null && p.Value.Equals(RespValueType.SimpleString));
        dict.ShouldContain(p => p.Key == "value" && p.Value != null && p.Value.Equals("OK"));
    }
}