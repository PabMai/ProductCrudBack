namespace ProductCrudBack.Wrappers;

public class ResponseResult<T>
{
    public bool IsSuccess { get; init; }
    public T? Data { get; init; }
    public T? Error { get; init; }

    public static ResponseResult<T> Success(T value) =>
        new() { IsSuccess = true, Data = value };

    public static ResponseResult<T> Fail(T error) =>
        new() { IsSuccess = false, Error = error };
}
