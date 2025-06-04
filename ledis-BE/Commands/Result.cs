namespace ledis_BE.Commands;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }

    private Result()
    {
    }
    
    public static Result<T> Ok()
    {
        return new Result<T>
        {
            IsSuccess = true,
            Data = default,
            Message = "OK",
        };
    }

    public static Result<T> Success(T? data)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Data = data,
            Message = null,
        };
    }

    public static Result<T> Fail(string message)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Data = default,
            Message = $"ERROR: {message}",
        };
    }
}