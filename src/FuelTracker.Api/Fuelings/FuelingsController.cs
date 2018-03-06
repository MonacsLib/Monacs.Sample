using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monacs.Core;
using static FuelTracker.Api.Fuelings.FuelingValidator;

namespace FuelTracker.Api.Fuelings
{
    [Route("api/[controller]")]
    public class FuelingsController : Controller
    {
        [HttpGet]
        public IEnumerable<Fueling> Get()
        {
            return new[] { new Fueling() };
        }

        [HttpGet("{id}")]
        public Fueling Get(string id)
        {
            return new Fueling();
        }

        [HttpPost]
        public void Post([FromBody]NewFuelingDto newFueling)
        {
            ValidateFuelingDto(newFueling)
            .Do(f => System.Console.WriteLine(f.When));
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody]EditFuelingDto updatedFueling)
        {
            ValidateFuelingDto(updatedFueling)
            .Do(f => System.Console.WriteLine(f.Id));
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
