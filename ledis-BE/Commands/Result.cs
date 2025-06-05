using ledis_BE.Resp;

namespace ledis_BE.Commands;

public class Result
{
    public bool IsSuccess { get; set; }
    public object? Data { get; set; }
    public string? Message { get; set; }

    private Result()
    {
    }
    
    public static Result Ok()
    {
        return new Result
        {
            IsSuccess = true,
            Data = null,
            Message = "OK",
        };
    }

    public static Result Success(RespValue? data)
    {
        return new Result
        {
            IsSuccess = true,
            Data = data?.GetValue(),
            Message = null,
        };
    }

    public static Result Fail(string message)
    {
        return new Result
        {
            IsSuccess = false,
            Data = default,
            Message = $"ERROR: {message}",
        };
    }
}