using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace aircraft_maintenance_api.Classes
{
    [DataContract]
    public class AircraftTask
    {
        [DataMember]
        public int ItemNumber { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime LogDate { get; set; }

        [DataMember]
        public int? LogHours { get; set; }

        [DataMember]
        public int? IntervalMonths { get; set; }

        [DataMember]
        public int? IntervalHours { get; set; }

        [DataMember]
        public DateTime? NextDue { get; set; }
    }
}
