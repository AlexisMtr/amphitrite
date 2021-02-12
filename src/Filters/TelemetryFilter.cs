using Amphitrite.Models;
using System.Linq;

namespace Amphitrite.Filters
{
    public class TelemetryFilter : TimeFilter<Telemetry>
    {
        public TelemetryType? Type { get; set; }

        public override IQueryable<Telemetry> Filter(IQueryable<Telemetry> source)
        {
            if (Type.HasValue)
                source = source.Where(e => e.Type.Equals(Type.Value));

            return base.Filter(source);
        }
    }
}
