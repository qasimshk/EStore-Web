namespace EStore.Domain.Common.Results;

using System.Net;

public class Result(bool isSuccess, List<string>? errorMessages, HttpStatusCode statusCode)
{
    public bool IsSuccess { get; } = isSuccess;
    public List<string>? ErrorMessages { get; } = errorMessages;
    public HttpStatusCode StatusCode { get; } = statusCode;

    public static Result SuccessResult() => new(true, null, HttpStatusCode.OK);

    public static Result FailedResult(List<string> errorMessages, HttpStatusCode statusCode)
        => new(false, errorMessages, statusCode);
}

public class Result<T>(bool isSuccess, T value, string errorMessage, HttpStatusCode statusCode) where T : class
{
    public bool IsSuccess { get; } = isSuccess;
    public T Value { get; } = value;
    public string ErrorMessage { get; } = errorMessage;
    public HttpStatusCode StatusCode { get; } = statusCode;

    public static Result<T> SuccessResult(T value) => new(true, value, null, HttpStatusCode.OK);

    public static Result<T> FailedResult(string errorMessage, HttpStatusCode statusCode)
        => new(false, null, errorMessage, statusCode);
}
