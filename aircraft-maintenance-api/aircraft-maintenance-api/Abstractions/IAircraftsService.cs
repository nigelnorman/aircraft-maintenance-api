using aircraft_maintenance_api.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aircraft_maintenance_api.Abstractions
{
    public interface IAircraftsService
    {
        Tuple<int, IEnumerable<AircraftTask>> CalculateNextDueDate(int aircraftId, IEnumerable<AircraftTask> tasks);
        void SetDate(DateTime? date);
    }
}
