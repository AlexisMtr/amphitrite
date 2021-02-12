using Amphitrite.Configuration;
using Amphitrite.Filters;
using Amphitrite.Helpers;
using Amphitrite.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Amphitrite.Repositories.SQL
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly AmphitriteContext context;

        public DeviceRepository(AmphitriteContext context)
        {
            this.context = context;
        }

        public void Add(Device entity)
        {
            context.Devices.Add(entity);
        }

        public int Count(IFilter<Device> filter)
        {
            return context.Devices
                .Count(filter ?? new DeviceFilter());
        }

        public void Delete(Device entity)
        {
            context.Devices.Remove(entity);
        }

        public IQueryable<Device> Get(IFilter<Device> filter)
        {
            return context.Devices
                .Where(filter ?? new DeviceFilter());
        }

        public Device GetById(string id, IFilter<Device> filter)
        {
            return context.Devices
                .Where(filter ?? new DeviceFilter())
                .Include(e => e.Configuration)
                .FirstOrDefault(e => e.DeviceId.Equals(id));
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
