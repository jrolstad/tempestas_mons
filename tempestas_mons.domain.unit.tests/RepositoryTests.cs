using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using tempestas_mons.domain.repositories;
using tempestas_mons.domain.services;

namespace tempestas_mons.domain.unit.tests
{
    [TestFixture]
    public class RepositoryTests
    {
        [Test]
        public void TravelTimesCanBeRead()
        {
            // Arrange
            var repo = new TravelTimeRepository(new StreamReaderFactory());

            // Act
            var result = repo.Get();

            // Assert
            var travelTimes = result.Where(r => r.End == DateTime.Today);
            foreach (var travelTime in travelTimes)
            {
                Console.WriteLine("{0}|{1}|{2}|{3}", travelTime.Start, travelTime.End, travelTime.Time, travelTime.Direction);
            }
            
        }

        [Test]
        public void RoadConditionsCanBeRead()
        {
            // Arrange
            var repo = new RoadConditionRepository(new StreamReaderFactory());

            // Act
            var result = repo.Get();

            // Assert
            var distinctList = result
                .SelectMany(r => r.TravelRestrictions)
                .SelectMany(r=>r.Restrictions)
                .Distinct()
                .OrderBy(t=>t)
                .ToList();

            foreach (var condition in distinctList)
            {
                Console.WriteLine(condition);
            }

        }
    }
}
