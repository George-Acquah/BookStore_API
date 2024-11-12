namespace BookStore.Application.Common
{
    public interface IBaseAPIResponse<T>
    {
        T? Data { get; set; }
        string? Message { get; set; }
        int StatusCode { get; set; }
    }

    public class BaseAPIResponse<T>(int _statusCode, string? _message, T? _data) : IBaseAPIResponse<T>
    {
        public int StatusCode { get; set; } = _statusCode;
        public string? Message { get; set; } = _message;
        public T? Data { get; set; } = _data;
    }
}
