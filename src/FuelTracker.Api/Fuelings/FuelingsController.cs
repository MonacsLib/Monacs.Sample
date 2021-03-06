﻿using System;
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
        public async Task<ApiResponse<IEnumerable<FuelingDisplayDto>>> GetAsync() =>
            await Storage.GetAll()
                .MapAsync(fs => fs.Select(FuelingMapper.MapToFuelingDisplayDto))
                .DoWhenErrorAsync(e => Logger.Warn(e.Message, e.Exception))
                .ToResponseAsync();

        [HttpGet("{fuelingId}")]
        public async Task<ApiResponse<FuelingDisplayDto>> Get(string fuelingId) =>
            await GuidParser.ParseGuid(fuelingId)
                .ToResult(Errors.Error($"Provided id was in incorrect format: {fuelingId}"))
                .BindAsync(id => Storage.Get(id))
                .MapAsync(FuelingMapper.MapToFuelingDisplayDto)
                .DoWhenErrorAsync(e => Logger.Warn(e.Message, e.Exception))
                .ToResponseAsync();

        [HttpPost]
        public async Task<ApiResponse<Guid>> Post([FromBody]FuelingEditDto newFueling) =>
            await ValidateFuelingDto(newFueling)
                .Map(FuelingMapper.MapToNewFueling)
                .BindAsync(fueling => Storage.Create(fueling))
                .MapAsync(fueling => fueling.Id)
                .DoWhenErrorAsync(e => Logger.Warn(e.Message, e.Exception))
                .ToResponseAsync();

        [HttpPut("{fuelingId}")]
        public async Task<ApiResponse<Unit>> Put(string fuelingId, [FromBody]FuelingEditDto updatedFueling) =>
            await GuidParser.ParseGuid(fuelingId)
                .ToResult(Errors.Error($"Provided id was in incorrect format: {fuelingId}"))
                .Bind(id => ValidateFuelingDto(updatedFueling).Map(fueling => (id: id, fueling: fueling)))
                .Map(x => FuelingMapper.MapToFueling(x.id, x.fueling))
                .BindAsync(fueling => Storage.Update(fueling.Id, fueling))
                .IgnoreAsync()
                .DoWhenErrorAsync(e => Logger.Warn(e.Message, e.Exception))
                .ToResponseAsync();

        [HttpDelete("{fuelingId}")]
        public async Task<ApiResponse<Unit>> Delete(string fuelingId) =>
            await GuidParser.ParseGuid(fuelingId)
                .ToResult(Errors.Error($"Provided id was in incorrect format: {fuelingId}"))
                .BindAsync(id => Storage.Delete(id))
                .DoWhenErrorAsync(e => Logger.Warn(e.Message, e.Exception))
                .ToResponseAsync();
    }
}
