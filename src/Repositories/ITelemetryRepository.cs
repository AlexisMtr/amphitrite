using Amphitrite.Filters;
using Amphitrite.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Amphitrite.Repositories
{
    public interface ITelemetryRepository : IRepository<Telemetry, int>
    {
        IEnumerable<Telemetry> GetByPool(int poolId, IFilter<Telemetry> filter, int rowsPerPage, int pageNumber, params Expression<Func<Telemetry, object>>[] includes);
        int CountByPool(int poolId, IFilter<Telemetry> filter);
        Telemetry GetLastTelemetry(int poolId, IFilter<Telemetry> filter);
    }
}
