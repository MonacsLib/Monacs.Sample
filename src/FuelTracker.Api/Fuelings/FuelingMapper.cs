using System;
using System.Collections.Generic;
using FuelTracker.Api.Shared;
using Monacs.Core;
using static Monacs.Core.Result;

namespace FuelTracker.Api.Fuelings
{
    public static class FuelingMapper
    {
        public static Fueling MapToNewFueling(FuelingDto dto) =>
            new Fueling {
                Id = Guid.NewGuid(),
                CarId = Guid.Parse(dto.CarId),
                When = DateTime.Parse(dto.When),
                FuelAmount = dto.FuelAmount,
                FuelPrice = dto.FuelPrice
            };
        
        public static Fueling MapToFueling(Guid id, FuelingDto dto) =>
            new Fueling {
                Id = id,
                CarId = Guid.Parse(dto.CarId),
                When = DateTime.Parse(dto.When),
                FuelAmount = dto.FuelAmount,
                FuelPrice = dto.FuelPrice
            };
    }
}
