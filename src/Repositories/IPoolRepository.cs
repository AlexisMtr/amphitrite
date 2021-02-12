using Amphitrite.Filters;
using Amphitrite.Models;
using System.Collections.Generic;

namespace Amphitrite.Repositories
{
    public interface IPoolRepository : IRepository<Pool, int>
    {
        IEnumerable<Pool> Get(IFilter<Pool> filter, int rowsPerPage, int pageNumber);
    }
}
