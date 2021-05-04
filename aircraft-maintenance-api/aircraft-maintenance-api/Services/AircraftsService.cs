using aircraft_maintenance_api.Abstractions;
using aircraft_maintenance_api.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aircraft_maintenance_api.Services
{
    public class AircraftsService : IAircraftsService
    {
        private readonly List<Aircraft> _aircraftRepository = new List<Aircraft>
        {
            new Aircraft
            {
                AircraftId = 1,
                DailyHours = 0.7f,
                CurrentHours = 550f
            },
            new Aircraft
            {
                AircraftId = 2,
                DailyHours = 1.1f,
                CurrentHours = 200f
            }
        };

        private DateTime _todaysDate;

        public void SetDate(DateTime? date)
        {
            _todaysDate = date ?? DateTime.Now;
        }

        public AircraftsService()
        {

        }

        public Tuple<int, IEnumerable<AircraftTask>> CalculateNextDueDate(int aircraftId, IEnumerable<AircraftTask> tasks)
        {
            var aircraft = _aircraftRepository.SingleOrDefault(a => a.AircraftId == aircraftId);
            var taskList = tasks.ToList();

            foreach(var task in taskList)
            {
                var intervalMonthsNextDueDate = IntervalMonthsNextDueDate(task.LogDate, task.IntervalMonths);
                var daysRemainingByHoursInterval = DaysRemainingByHoursInterval(task.LogHours, task.IntervalHours, aircraft);
                var intervalHoursNextDueDate = IntervalHoursNextDueDate(daysRemainingByHoursInterval);
                var canCalculateNextDue = intervalMonthsNextDueDate.HasValue || intervalHoursNextDueDate.HasValue;
                if (!canCalculateNextDue)
                {
                    task.NextDue = null;
                    continue;
                }
                if (intervalMonthsNextDueDate.HasValue && intervalHoursNextDueDate.HasValue)
                {
                    task.NextDue = new DateTime(Math.Min(intervalMonthsNextDueDate.Value.Ticks, intervalHoursNextDueDate.Value.Ticks)).Date;
                }
                else
                {
                    task.NextDue = intervalMonthsNextDueDate ?? intervalHoursNextDueDate;
                }
            }
            var sorted = taskList.OrderByDescending(t => t.NextDue.HasValue).ThenBy(t => t.NextDue).ThenBy(t => t.Description).ToList();

            return new Tuple<int, IEnumerable<AircraftTask>> (aircraftId, sorted);
        }

        private DateTime? IntervalMonthsNextDueDate(DateTime? logDate, int? intervalMonths)
        {
            var noNullVals = logDate.HasValue && intervalMonths.HasValue;
            if (!noNullVals) return null;

            return logDate.Value.AddMonths(intervalMonths.Value);
        }

        private float? DaysRemainingByHoursInterval(int? logHours, int? intervalHours, Aircraft aircraft)
        {
            var remaining = ((logHours ?? 0) + (intervalHours ?? 0) - aircraft.CurrentHours) / aircraft.DailyHours;

            if (remaining < 0) return null;

            return remaining;
        }

        private DateTime? IntervalHoursNextDueDate(float? daysRemainingByHoursInterval)
        {
            if (!daysRemainingByHoursInterval.HasValue) return null;

            return _todaysDate.AddDays(daysRemainingByHoursInterval.Value).Date;
        }
    }
}
