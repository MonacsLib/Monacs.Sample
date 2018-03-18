using System;
using System.Collections.Generic;

namespace FuelTracker.Api.Fuelings
{
    public class FuelingEditDto
    {
        public string CarId { get; set; }
        public string When { get; set; }
        public decimal FuelAmount { get; set; }
        public decimal FuelPrice { get; set; }
    }

    public class FuelingDisplayDto
    {
        public string Id { get; set; }
        public string CarId { get; set; }
        public string When { get; set; }
        public decimal FuelAmount { get; set; }
        public decimal FuelPrice { get; set; }
    }
}
