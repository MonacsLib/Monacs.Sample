using System;
using System.Collections.Generic;
using FuelTracker.Api.Shared;
using Monacs.Core;
using static Monacs.Core.Result;

namespace FuelTracker.Api.Cars
{
    public static class CarMapper
    {
        public static Car MapToNewCar(CarEditDto dto) =>
            new Car {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Make = dto.Make,
                Model = dto.Model,
                PrimaryFuel = Enum.Parse<FuelType>(dto.PrimaryFuel),
                SecondaryFuel = dto.SecondaryFuel.ToOption().Map(f => Enum.Parse<FuelType>(f))
            };
        
        public static Car MapToCar(Guid id, CarEditDto dto) =>
            new Car {
                Id = id,
                Name = dto.Name,
                Make = dto.Make,
                Model = dto.Model,
                PrimaryFuel = Enum.Parse<FuelType>(dto.PrimaryFuel),
                SecondaryFuel = dto.SecondaryFuel.ToOption().Map(f => Enum.Parse<FuelType>(f))
            };
        
        public static CarDisplayDto MapToCarDisplayDto(Car model) =>
            new CarDisplayDto {
                Id = model.Id.ToString(),
                Name = model.Name,
                Make = model.Make,
                Model = model.Model,
                PrimaryFuel = model.PrimaryFuel.ToString(),
                SecondaryFuel = model.SecondaryFuel.Map(f => f.ToString()).GetOrDefault()
            };
    }
}
