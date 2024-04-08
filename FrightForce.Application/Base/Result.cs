namespace FrightForce.Application.Base;

public class Result
{
    public bool Success { get; private set; }
    public string ErrorMessage { get; private set; }

    protected Result(bool success, string errorMessage)
    {
        Success = success;
        ErrorMessage = errorMessage;
    }

    public static Result Fail(string message) => new Result(false, message);
    public static Result Ok() => new Result(true, null);
}

public class Result<T> : Result
{
    public T Value { get; private set; }

    private Result(T value, bool success, string errorMessage) : base(success, errorMessage)
    {
        Value = value;
    }

    public static Result<T> Fail(string message) => new Result<T>(default, false, message);
    public static Result<T> Ok(T value) => new Result<T>(value, true, null);
}