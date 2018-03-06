using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelTracker.Api.Shared
{
    public interface IModel
    {
        Guid Id { get; set; }
    }

    public enum FuelType
    {
        Petrol,
        Diesel,
        LPG,
        CNG
    }
}
