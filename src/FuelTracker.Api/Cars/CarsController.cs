using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelTracker.Api.Shared;
using Microsoft.AspNetCore.Mvc;
using Monacs.Core;
using Monacs.Core.Async;
using Monacs.Core.Unit;

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
        public async Task<ApiResponse<IEnumerable<Car>>> Get() =>
            await Storage.GetAll()
                // TODO add conversion to dto
                // TODO add logging
                .ToResponseAsync();

        [HttpGet("{carId}")]
        public async Task<ApiResponse<Car>> Get(string carId) =>
            await GuidParser.ParseGuid(carId)
                .ToResult(Errors.Error($"Provided id was in incorrect format: {carId}"))
                .BindAsync(id => Storage.GetAsync(id))
                // TODO add conversion to dto
                // TODO add logging
                .ToResponseAsync();

        [HttpPost]
        public async Task<ApiResponse<Guid>> Post([FromBody]Car newCar) =>
            await Storage.Create(newCar)
                .MapAsync(car => car.Id)
                // TODO add logging
                .ToResponseAsync();

        [HttpPut("{carId}")]
        public async Task<ApiResponse<Unit>> Put(string carId, [FromBody]Car updatedCar) =>
            await GuidParser.ParseGuid(carId)
                .ToResult(Errors.Error($"Provided id was in incorrect format: {carId}"))
                .BindAsync(id => Storage.Update(id, updatedCar))
                .IgnoreAsync()
                // TODO add logging
                .ToResponseAsync();

        [HttpDelete("{carId}")]
        public async Task<ApiResponse<Unit>> Delete(string carId) =>
            await GuidParser.ParseGuid(carId)
                .ToResult(Errors.Error($"Provided id was in incorrect format: {carId}"))
                .BindAsync(id => Storage.Delete(id))
                // TODO add logging
                .ToResponseAsync();
    }
}
