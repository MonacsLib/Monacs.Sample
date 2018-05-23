using System;
using System.Threading.Tasks;
using Monacs.Core;

namespace FuelTracker.Api.Shared
{
    public static class ResultExtensions
    {
        public static ApiResponse<T> ToResponse<T>(this Result<T> result) =>
            result.Match(ok: x => ApiResponse<T>.Success(x), error: e => ApiResponse<T>.Failure(e));

        public static async Task<ApiResponse<T>> ToResponseAsync<T>(this Task<Result<T>> result) =>
            await result.MatchAsync(ok: x => ApiResponse<T>.Success(x), error: e => ApiResponse<T>.Failure(e));
    }
}
