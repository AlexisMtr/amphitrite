using Amphitrite.Filters;
using Amphitrite.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Amphitrite.Repositories
{
    public interface IAlarmRepository : IRepository<Alarm, int>
    {
        IEnumerable<Alarm> GetByPool(int poolId, IFilter<Alarm> filter, int rowsPerPage, int pageNumber, params Expression<Func<Alarm, object>>[] includes);
        int CountByPool(int poolId, IFilter<Alarm> filter);
    }
}
