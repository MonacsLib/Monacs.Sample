using System;
using System.Collections.Generic;
using FuelTracker.Api.Shared;

namespace FuelTracker.Api.Fuelings
{
    public class Fueling : IModel
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public DateTime When { get; set; }
        public decimal FuelAmount { get; set; }
        public decimal FuelPrice { get; set; }
    }
}
