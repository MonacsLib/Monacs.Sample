using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuelTracker.Api.Shared;
using Microsoft.AspNetCore.Mvc;
using Monacs.Core;
using Monacs.Core.Unit;
using static FuelTracker.Api.Fuelings.FuelingValidator;

namespace FuelTracker.Api.Fuelings
{
    [Route("api/[controller]")]
    public class FuelingsController : Controller
    {
        private readonly FuelingStorage Storage;

        public FuelingsController()
        {
            Storage = new FuelingStorage();
        }

        [HttpGet]
        public ApiResponse<IEnumerable<Fueling>> Get() =>
            Storage.GetAll()
                // TODO add conversion to dto
                // TODO add logging
                .ToResponse();

        [HttpGet("{fuelingId}")]
        public ApiResponse<Fueling> Get(string fuelingId) =>
            GuidParser.ParseGuid(fuelingId)
                .ToResult(Errors.Error($"Provided id was in incorrect format: {fuelingId}"))
                .Bind(id => Storage.Get(id))
                // TODO add conversion to dto
                // TODO add logging
                .ToResponse();

        [HttpPost]
        public ApiResponse<Guid> Post([FromBody]FuelingDto newFueling) =>
            ValidateFuelingDto(newFueling)
                .Map(FuelingMapper.MapToNewFueling)
                .Bind(fueling => Storage.Create(fueling))
                .Map(fueling => fueling.Id)
                // TODO add logging
                .ToResponse();

        [HttpPut("{fuelingId}")]
        public ApiResponse<Unit> Put(string fuelingId, [FromBody]FuelingDto updatedFueling) =>
            GuidParser.ParseGuid(fuelingId)
                .ToResult(Errors.Error($"Provided id was in incorrect format: {fuelingId}"))
                .Bind(id => ValidateFuelingDto(updatedFueling).Map(fueling => (id: id, fueling: fueling)))
                .Map(x => FuelingMapper.MapToFueling(x.id, x.fueling))
                .Bind(fueling => Storage.Update(fueling.Id, fueling))
                .Ignore()
                // TODO add logging
                .ToResponse();

        [HttpDelete("{fuelingId}")]
        public void Delete(string fuelingId) =>
            GuidParser.ParseGuid(fuelingId)
                .ToResult(Errors.Error($"Provided id was in incorrect format: {fuelingId}"))
                .Bind(id => Storage.Delete(id))
                // TODO add logging
                .ToResponse();
    }
}
