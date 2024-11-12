namespace BookStore.Application.Common
{
    public interface IRepositoryResponse<T>
    {
        T? Data { get; }
        string? ErrorMessage { get; }
        bool Success { get; }
    }

    public class RepositoryResponse<T> : IRepositoryResponse<T>
    {
        public bool Success { get; private set; }
        public T? Data { get; private set; }
        public string? ErrorMessage { get; private set; }

        private RepositoryResponse(bool success, T? data, string? errorMessage)
        {
            Success = success;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public static RepositoryResponse<T> SuccessResult(T? data) => new(true, data, null);
        public static RepositoryResponse<T> FailureResult(string errorMessage) => new(false, default, errorMessage);
    }
}
