using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace aircraft_maintenance_api.Classes
{
    [DataContract]
    public class Payload
    {
        [DataMember]
        public IEnumerable<AircraftTask> Tasks { get; set; }
    }
}
