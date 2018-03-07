using System;
using Monacs.Core;

namespace FuelTracker.Api.Shared
{
    public static class ResultExtensions
    {
        public static ApiResponse<T> ToResponse<T>(this Result<T> result) =>
            result.Match(ok: x => ApiResponse<T>.Success(x), error: e => ApiResponse<T>.Failure(e));
    }
}
