using System;
using System.Collections.Generic;
using Monacs.Core;
using static Monacs.Core.Result;

namespace FuelTracker.Api.Fuelings
{
    public static class FuelingValidator
    {
        public static Result<FuelingEditDto> ValidateFuelingDto(FuelingEditDto fueling) =>
            fueling
            .ToResult(() => Errors.Error("Fueling cannot be null"))
            .Bind(f => f.FuelAmount <= 0 ? Error<FuelingEditDto>(Errors.Error("Fuel amount must be > 0")) : Ok(f))
            .Bind(f => f.FuelPrice <= 0 ? Error<FuelingEditDto>(Errors.Error("Fuel price must be > 0")) : Ok(f));
    }
}
