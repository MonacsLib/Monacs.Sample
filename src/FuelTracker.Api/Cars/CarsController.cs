using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelTracker.Api.Shared;
using Microsoft.AspNetCore.Mvc;
using Monacs.Core;
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
        public ApiResponse<IEnumerable<Car>> Get() =>
            Storage.GetAll()
                // TODO add conversion to dto
                // TODO add logging
                .ToResponse();

        [HttpGet("{carId}")]
        public ApiResponse<Car> Get(string carId) =>
            GuidParser.ParseGuid(carId)
                .ToResult(Errors.Error($"Provided id was in incorrect format: {carId}"))
                .Bind(id => Storage.Get(id))
                // TODO add conversion to dto
                // TODO add logging
                .ToResponse();

        [HttpPost]
        public ApiResponse<Guid> Post([FromBody]Car newCar) =>
            Storage.Create(newCar)
                .Map(car => car.Id)
                // TODO add logging
                .ToResponse();

        [HttpPut("{carId}")]
        public ApiResponse<Unit> Put(string carId, [FromBody]Car updatedCar) =>
            GuidParser.ParseGuid(carId)
                .ToResult(Errors.Error($"Provided id was in incorrect format: {carId}"))
                .Bind(id => Storage.Update(id, updatedCar))
                .Ignore()
                // TODO add logging
                .ToResponse();

        [HttpDelete("{carId}")]
        public ApiResponse<Unit> Delete(string carId) =>
            GuidParser.ParseGuid(carId)
                .ToResult(Errors.Error($"Provided id was in incorrect format: {carId}"))
                .Bind(id => Storage.Delete(id))
                // TODO add logging
                .ToResponse();
    }
}
