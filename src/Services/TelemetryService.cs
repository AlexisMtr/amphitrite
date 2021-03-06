﻿using Amphitrite.Filters;
using Amphitrite.Helpers;
using Amphitrite.Models;
using Amphitrite.Repositories;
using System.Collections.Generic;

namespace Amphitrite.Services
{
    public class TelemetryService
    {
        private readonly ITelemetryRepository telemetryRepository;

        public TelemetryService(ITelemetryRepository telemetryRepository)
        {
            this.telemetryRepository = telemetryRepository;
        }

        public Telemetry GetCurrent(int poolId, TelemetryType telemetryType)
        {
            TelemetryFilter filter = new TelemetryFilter { Type = telemetryType };
            return telemetryRepository.GetLastTelemetry(poolId, filter);
        }

        public IEnumerable<Telemetry> GetAllCurrent(int poolId)
        {
            List<Telemetry> telemetries = new List<Telemetry>
            {
                GetCurrent(poolId, TelemetryType.Level),
                GetCurrent(poolId, TelemetryType.Temperature),
                GetCurrent(poolId, TelemetryType.Battery),
                GetCurrent(poolId, TelemetryType.Ph)
            };

            telemetries.RemoveAll(e => e == null);

            return telemetries;
        }

        public PaginatedElement<Telemetry> GetByPool(int poolId, IFilter<Telemetry> filter, int rowsPerPage, int pageNumber)
        {
            IEnumerable<Telemetry> telemetries = telemetryRepository.GetByPool(poolId, filter, rowsPerPage, pageNumber);
            int totalElementCount = telemetryRepository.CountByPool(poolId, filter);

            return new PaginatedElement<Telemetry>
            {
                TotalElementCount = totalElementCount,
                Elements = telemetries,
                PageCount = RestApiHelper.GetPageCount(totalElementCount, rowsPerPage)
            };
        }
    }
}
