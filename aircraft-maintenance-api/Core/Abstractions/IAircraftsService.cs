using Aircraft.Maintenance.Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircraft.Maintenance.Core.Abstractions
{
    public interface IAircraftsService
    {
        Tuple<int, IEnumerable<AircraftTask>> CalculateNextDueDate(int aircraftId, IEnumerable<AircraftTask> tasks);
        void SetDate(DateTime? date);
    }
}
