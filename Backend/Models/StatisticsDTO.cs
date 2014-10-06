using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class StatisticsDTO
    {
        [JsonProperty(PropertyName = "allSubscriptions")]
        public long AllSubscriptions { get; set; }

        [JsonProperty(PropertyName = "lastDaySubscriptions")]
        public long LastDaySubscriptions { get; set; }
    }
}