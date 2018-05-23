using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Monacs.Core;
using static Monacs.Core.Option;

namespace FuelTracker.Api.Shared
{
    public static class GuidParser
    {
        public static Option<Guid> ParseGuid(string guidString) =>
            !string.IsNullOrEmpty(guidString) && Guid.TryParse(guidString, out var guid)
            ? Some(guid)
            : None<Guid>();
    }
}
