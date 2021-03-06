﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amphitrite.Models
{
    public class Pool
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TemperatureMinValue { get; set; }
        public double TemperatureMaxValue { get; set; }
        public double PhMinValue { get; set; }
        public double PhMaxValue { get; set; }
        public double WaterLevelMinValue { get; set; }
        public double WaterLevelMaxValue { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [ForeignKey("DeviceId")]
        public Device Device { get; set; }

        public ICollection<Telemetry> Telemetries { get; set; }
        public ICollection<Alarm> Alarms { get; set; }
        public ICollection<UserPoolAssociation> Users { get; set; }
    }
}
