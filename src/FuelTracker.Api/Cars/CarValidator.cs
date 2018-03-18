using System;
using System.Collections.Generic;
using FuelTracker.Api.Shared;
using Monacs.Core;
using static Monacs.Core.Result;

namespace FuelTracker.Api.Cars
{
    public static class CarValidator
    {
        public static Result<CarEditDto> ValidateCarDto(CarEditDto Car) =>
            Car
            .ToResult(() => Errors.Error("Car cannot be null"))
            .Bind(f => string.IsNullOrEmpty(f.Name) ? Error<CarEditDto>(Errors.Error("Name must be filled in")) : Ok(f))
            .Bind(f => string.IsNullOrEmpty(f.Make) ? Error<CarEditDto>(Errors.Error("Make must be filled in")) : Ok(f))
            .Bind(f => string.IsNullOrEmpty(f.Model) ? Error<CarEditDto>(Errors.Error("Model must be filled in")) : Ok(f))
            .Bind(f => string.IsNullOrEmpty(f.PrimaryFuel) ? Error<CarEditDto>(Errors.Error("PrimaryFuel must be filled in")) : Ok(f))
            .Bind(f => !Enum.TryParse<FuelType>(f.PrimaryFuel, out var _) ? Error<CarEditDto>(Errors.Error("PrimaryFuel must have correct value")) : Ok(f))
            .Bind(f => !string.IsNullOrEmpty(f.SecondaryFuel) && !Enum.TryParse<FuelType>(f.SecondaryFuel, out var _) ? Error<CarEditDto>(Errors.Error("SecondaryFuel must have correct value")) : Ok(f));
    }
}
