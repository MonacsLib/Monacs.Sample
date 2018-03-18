using System;
using System.Collections.Generic;
using FuelTracker.Api.Shared;

namespace FuelTracker.Api.Cars
{
    public class CarEditDto
    {
        public string Name { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string PrimaryFuel { get; set; }
        public string SecondaryFuel { get; set; }
    }

    public class CarDisplayDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string PrimaryFuel { get; set; }
        public string SecondaryFuel { get; set; }
    }
}
