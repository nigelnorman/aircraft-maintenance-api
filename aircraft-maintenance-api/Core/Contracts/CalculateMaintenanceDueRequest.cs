using Aircraft.Maintenance.Core.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aircraft.Maintenance.Core.Contracts
{
    public interface CalculateMaintenanceDueRequest
    {
        int AircraftId { get; }

        Payload Payload { get; }
    }
}
