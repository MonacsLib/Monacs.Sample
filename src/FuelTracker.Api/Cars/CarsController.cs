using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelTracker.Api.Shared;
using Microsoft.AspNetCore.Mvc;
using Monacs.Core;
using Monacs.Core.Unit;
using static FuelTracker.Api.Cars.CarValidator;

namespace FuelTracker.Api.Cars
{
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        private readonly CarStorage Storage;

        public CarsController()
        {
            Storage = new CarStorage();
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<CarDisplayDto>>> Get() =>
            await Storage.GetAll()
                .MapAsync(fs => fs.Select(CarMapper.MapToCarDisplayDto))
                .DoWhenErrorAsync(e => Logger.Warn(e.Message, e.Exception))
                .ToResponseAsync();

        [HttpGet("{carId}")]
        public async Task<ApiResponse<CarDisplayDto>> Get(string carId) =>
            await GuidParser.ParseGuid(carId)
                .ToResult(Errors.Error($"Provided id was in incorrect format: {carId}"))
                .BindAsync(id => Storage.Get(id))
                .MapAsync(CarMapper.MapToCarDisplayDto)
                .DoWhenErrorAsync(e => Logger.Warn(e.Message, e.Exception))
                .ToResponseAsync();

        [HttpPost]
        public async Task<ApiResponse<Guid>> Post([FromBody]CarEditDto newCar) =>
            await ValidateCarDto(newCar)
                .Map(CarMapper.MapToNewCar)
                .BindAsync(car => Storage.Create(car))
                .MapAsync(car => car.Id)
                .DoWhenErrorAsync(e => Logger.Warn(e.Message, e.Exception))
                .ToResponseAsync();

        [HttpPut("{carId}")]
        public async Task<ApiResponse<Unit>> Put(string carId, [FromBody]CarEditDto updatedCar) =>
            await GuidParser.ParseGuid(carId)
                .ToResult(Errors.Error($"Provided id was in incorrect format: {carId}"))
                .Bind(id => ValidateCarDto(updatedCar).Map(car => (id: id, car: car)))
                .Map(x => CarMapper.MapToCar(x.id, x.car))
                .BindAsync(car => Storage.Update(car.Id, car))
                .IgnoreAsync()
                .DoWhenErrorAsync(e => Logger.Warn(e.Message, e.Exception))
                .ToResponseAsync();

        [HttpDelete("{carId}")]
        public async Task<ApiResponse<Unit>> Delete(string carId) =>
            await GuidParser.ParseGuid(carId)
                .ToResult(Errors.Error($"Provided id was in incorrect format: {carId}"))
                .BindAsync(id => Storage.Delete(id))
                .DoWhenErrorAsync(e => Logger.Warn(e.Message, e.Exception))
                .ToResponseAsync();
    }
}
