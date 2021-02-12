using Amphitrite.Configuration;
using Amphitrite.Filters;
using Amphitrite.Helpers;
using Amphitrite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Amphitrite.Repositories.SQL
{
    public class PoolRepository : IPoolRepository
    {
        private readonly AmphitriteContext context;

        public PoolRepository(AmphitriteContext context)
        {
            this.context = context;
        }

        public void Add(Pool entity)
        {
            context.Pools.Add(entity);
        }

        public int Count(IFilter<Pool> filter)
        {
            return context.Pools
                .Count(filter ?? new PoolFilter());
        }

        public void Delete(Pool entity)
        {
            context.Pools.Remove(entity);
        }

        public IEnumerable<Pool> Get(IFilter<Pool> filter, int rowsPerPage, int pageNumber)
        {
            int skip = Math.Max(0, pageNumber - 1) * rowsPerPage;

            return context.Pools
                .Include(e => e.Device)
                .Include(e => e.Alarms)
                .Where(filter ?? new PoolFilter())
                .OrderBy(e => e.Name)
                .Skip(skip)
                .Take(rowsPerPage);
        }

        public IQueryable<Pool> Get(IFilter<Pool> filter)
        {
            return context.Pools
                .Include(e => e.Device)
                .Include(e => e.Alarms)
                .Where(filter ?? new PoolFilter());
        }

        public Pool GetById(int id, IFilter<Pool> filter)
        {
            return context.Pools
                .Include(e => e.Device)
                .Include(e => e.Alarms)
                .Where(filter ?? new PoolFilter())
                .Include(e => e.Telemetries)
                .FirstOrDefault(e => e.Id == id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
