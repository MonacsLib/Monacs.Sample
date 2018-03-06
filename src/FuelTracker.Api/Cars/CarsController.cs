using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelTracker.Api.Shared;
using Microsoft.AspNetCore.Mvc;
using Monacs.Core;

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
        public IEnumerable<Car> Get() =>
            Storage.GetAll()
                .GetOrDefault();

        [HttpGet("{id}")]
        public Car Get(string carId) =>
            GuidParser.ParseGuid(carId)
                .ToResult(Errors.Error($"Provided id was in incorrect format: {carId}"))
                .Bind(id => Storage.Get(id))
                .GetOrDefault();

        [HttpPost]
        public void Post([FromBody]Car newCar)
        {
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Car updatedCar)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
