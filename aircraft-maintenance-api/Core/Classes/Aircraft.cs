using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircraft.Maintenance.Core.Classes
{
    public class Aircraft
    {
        public int AircraftId { get; set; }

        public float DailyHours { get; set; }

        public float CurrentHours { get; set; }
    }
}
