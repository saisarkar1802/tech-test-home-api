namespace HouseUnitAPI.Helpers.ExceptionHandling
{
    public class ReturnResult<T>
    {
        public T Data { get; set; }
        public ErrorResponse Error { get; set; }
        public bool IsSuccess => Error == null;

        public static ReturnResult<T> Success(T data) => new ReturnResult<T> { Data = data };
        public static ReturnResult<T> Failure(string errorMessage, int statusCode) => new ReturnResult<T> { Error = new ErrorResponse(errorMessage, statusCode) };
    }
}
