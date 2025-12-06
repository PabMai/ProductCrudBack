using Azure;

namespace ProductCrudBack.Wrappers;

public class ResponseResult
{
    public static object ResponseValue(object data)
    {
        return new
        {
            status = true,
            data = data
        };
    }

    public static object ResponseError(object error)
    {
        return new
        {
            status = false,
            error = error
        };
    }
}