using aircraft_maintenance_api.Abstractions;
using aircraft_maintenance_api.Classes;
using aircraft_maintenance_api.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace aircraft_maintenance_api.Tests.Given_AircraftsService
{
    public class When_Given_Aircraft1
    {
        private readonly IAircraftsService subject;
        private readonly DateTime fakeToday = new DateTime(2018, 06, 19, 0, 0, 0);
        private readonly int fakeAircraftId = 1;
        private readonly List<AircraftTask> fakeTasks = new List<AircraftTask>
        {
            new AircraftTask
            {
                ItemNumber = 1,
                Description = "Item 1",
                LogDate = new DateTime(2018, 04, 07, 0, 0, 0),
                LogHours = null,
                IntervalMonths = null,
                IntervalHours = null
            },
            new AircraftTask
            {
                ItemNumber = 2,
                Description = "Item 2",
                LogDate = new DateTime(2018, 04, 07, 0, 0, 0),
                LogHours = 100,
                IntervalMonths = 12,
                IntervalHours = 500
            },
            new AircraftTask
            {
                ItemNumber = 3,
                Description = "Item 3",
                LogDate = new DateTime(2018, 06, 01, 0, 0, 0),
                LogHours = 150,
                IntervalMonths = null,
                IntervalHours = 400
            },
            new AircraftTask
            {
                ItemNumber = 4,
                Description = "Item 4",
                LogDate = new DateTime(2018, 06, 01, 0, 0, 0),
                LogHours = 150,
                IntervalMonths = 6,
                IntervalHours = null
            },
        };
        private readonly Tuple<int, IEnumerable<AircraftTask>> expected = 
            new Tuple<int, IEnumerable<AircraftTask>>( 1, new List<AircraftTask>
            {
                new AircraftTask
                {
                    ItemNumber = 3,
                    NextDue = DateTime.Parse("06/19/2018")
                },                
                new AircraftTask
                {
                    ItemNumber = 2,
                    NextDue = DateTime.Parse("08/29/2018")
                },                
                new AircraftTask
                {
                    ItemNumber = 4,
                    NextDue = DateTime.Parse("12/01/2018")
                },                
                new AircraftTask
                {
                    ItemNumber = 1,
                    NextDue = null
                },
            });
        private Tuple<int, IEnumerable<AircraftTask>> actual;

        public When_Given_Aircraft1() 
        {
            subject = new AircraftsService();
            subject.SetDate(fakeToday);
            actual = subject.CalculateNextDueDate(fakeAircraftId, fakeTasks);
        }

        [Fact]
        public void Then_Return_NextDueDate_and_Sort_Order()
        {
            actual.Item1.Should().Be(expected.Item1);
            //actual.Item2.Should().BeEquivalentTo(expected.Item2);
            actual.Item2.Select(i => i.ItemNumber).Should().BeEquivalentTo(expected.Item2.Select(i => i.ItemNumber));
            actual.Item2.Select(i => i.NextDue).Should().BeEquivalentTo(expected.Item2.Select(i => i.NextDue));
        }
    }
}
