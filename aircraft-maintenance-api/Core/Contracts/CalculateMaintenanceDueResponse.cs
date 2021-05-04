using Aircraft.Maintenance.Core.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aircraft.Maintenance.Core.Contracts
{
    public interface CalculateMaintenanceDueResponse
    {
        int AircraftId { get; }

        IEnumerable<AircraftTask> Tasks { get; }
    }
}
