namespace Kalmia.Core.Common;

internal static class ResultDefaults
{
    public const string NotFoundMessage = "Resource not found.";
}

public enum ResultErrorType
{
    None,
    Validation,
    NotFound
}

public record ErrorDetail(string Code, string Description, string? Field = null);

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public IReadOnlyList<ErrorDetail> Errors { get; }
    public ResultErrorType ErrorType { get; }

    private Result(bool isSuccess, T? value, IReadOnlyList<ErrorDetail> errors, ResultErrorType errorType)
    {
        IsSuccess = isSuccess;
        Value = value;
        Errors = errors;
        ErrorType = errorType;
    }

    public static Result<T> Success(T value) =>
        new(true, value, Array.Empty<ErrorDetail>(), ResultErrorType.None);

    public static Result<T> Failure(params ErrorDetail[] errors) =>
        new(false, default, errors, ResultErrorType.Validation);

    public static Result<T> Failure(IEnumerable<ErrorDetail> errors) =>
        new(false, default, errors.ToList(), ResultErrorType.Validation);

    public static Result<T> NotFound(string msg = ResultDefaults.NotFoundMessage) =>
        new(false, default, new[] { new ErrorDetail("NOT_FOUND", msg) }, ResultErrorType.NotFound);
}

public static class Result
{
    public static Result<Unit> Success() => Result<Unit>.Success(Unit.Value);
    public static Result<Unit> Failure(params ErrorDetail[] errors) => Result<Unit>.Failure(errors);
    public static Result<Unit> Failure(IEnumerable<ErrorDetail> errors) => Result<Unit>.Failure(errors);
    public static Result<Unit> NotFound(string msg = ResultDefaults.NotFoundMessage) => Result<Unit>.NotFound(msg);
}