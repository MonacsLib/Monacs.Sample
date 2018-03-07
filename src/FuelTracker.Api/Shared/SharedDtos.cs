using System;
using Monacs.Core;

namespace FuelTracker.Api.Shared
{
    public class ApiResponse<T>
    {
        private ApiResponse(T value)
        {
            IsSuccess = true;
            Data = value;
        }

        private ApiResponse(ErrorDetails error)
        {
            Error = new ErrorMessage(error.Level.ToString().ToLower(), error.Message.GetOrDefault(), error.Key.GetOrDefault(), error.Metadata.GetOrDefault());
        }

        public static ApiResponse<T> Success(T value) => new ApiResponse<T>(value);

        public static ApiResponse<T> Failure(ErrorDetails error) => new ApiResponse<T>(error);

        public T Data { get; }
        public bool IsSuccess { get; }
        public ErrorMessage? Error { get; }

        public struct ErrorMessage
        {
            public ErrorMessage(string level, string message, string key, object metadata)
            {
                Level = level;
                Message = message;
                Key = key;
                Metadata = metadata;
            }

            public string Level { get; }
            public string Message { get; }
            public string Key { get; }
            public object Metadata { get; }
        }
    }
}
