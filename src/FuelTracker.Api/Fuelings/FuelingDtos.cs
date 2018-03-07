using System;
using System.Collections.Generic;

namespace FuelTracker.Api.Fuelings
{
    public class FuelingDto
    {
        public string CarId { get; set; }
        public string When { get; set; }
        public decimal FuelAmount { get; set; }
        public decimal FuelPrice { get; set; }
    }
}
