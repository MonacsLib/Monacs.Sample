using System;
using System.Collections.Generic;
using FuelTracker.Api.Shared;
using Monacs.Core;

namespace FuelTracker.Api.Cars
{
    public class Car : IModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public FuelType PrimaryFuel { get; set; }
        public Option<FuelType> SecondaryFuel { get; set; }
    }
}
