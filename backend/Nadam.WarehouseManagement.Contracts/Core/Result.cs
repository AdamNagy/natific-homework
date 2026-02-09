namespace Nadam.WarehouseManagement.Contracts.Core;

/// <summary>
/// Represents an operations result.
/// </summary>
/// <param name="IsSuccess">Indicates weather the operation was a success or not.</param>
/// <param name="Error">If the operation failed, error contains user friendly message, not the <see cref="Exception"/> message</param>
public record Result(bool IsSuccess, string? Error)
{
    public static Result Success() => new Result(true, null);

    public static Result Failed(string error) => new Result(false, error);
}


public record Result<T>(bool IsSuccess, string? Error, T? Payload)
{
    public static Result<T> Success<T>(T result) => new Result<T>(true, null, result);

    public static Result<T> Failed<T>(string error) => new Result<T>(false, error, default);
}
