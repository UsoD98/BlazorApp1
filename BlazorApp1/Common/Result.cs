namespace BlazorApp1.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public string? ErrorMessage { get; }
        public T? Value { get; }

        private Result(bool isSuccess, T? value, string? errorMessage)
        {
            IsSuccess = isSuccess;
            Value = value;
            ErrorMessage = errorMessage;
        }

        public static Result<T> Success(T value) => new(true, value, null);
        public static Result<T> Failure(string message) => new(false, default, message);
    }
}
