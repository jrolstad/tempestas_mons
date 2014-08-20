using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using tempestas_mons.domain.repositories;

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
    }
}
