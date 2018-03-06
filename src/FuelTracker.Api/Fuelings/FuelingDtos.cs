using System;
using System.Collections.Generic;

namespace FuelTracker.Api.Fuelings
{
    public abstract class BaseFuelingDto
    {
        public string CarId { get; set; }
        public string When { get; set; }
        public decimal FuelAmount { get; set; }
        public decimal FuelPrice { get; set; }
    }

    public class NewFuelingDto : BaseFuelingDto
    {
    }

    public class EditFuelingDto : BaseFuelingDto
    {
        public string Id { get; set; }
    }
}
