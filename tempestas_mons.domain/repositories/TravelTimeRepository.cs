using System;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using tempestas_mons.domain.models;
using tempestas_mons.domain.models.rawdata;

namespace tempestas_mons.domain.repositories
{
    public class TravelTimeRepository
    {
        private readonly StreamReaderFactory _streamReaderFactory;

        public TravelTimeRepository(StreamReaderFactory streamReaderFactory)
        {
            _streamReaderFactory = streamReaderFactory;
        }

        public List<TravelTime> Get()
        {
            const string eastboundFileName = "tempestas_mons.domain.data.csv.TravelTime_I-90_EB.csv";
            var eastboundData = ReadDataFromFile(eastboundFileName,Direction.Eastbound);

            const string westboundFileName = "tempestas_mons.domain.data.csv.TravelTime_I-90_WB.csv";
            var westboundData = ReadDataFromFile(westboundFileName,Direction.Westbound);

            var travelTimes = eastboundData
                .Union(westboundData)
                .ToList();

            return travelTimes;
        }

        private List<TravelTime> ReadDataFromFile(string fileName, Direction direction)
        {
            using (var reader = _streamReaderFactory.GetEmbeddedFile(fileName))
            using ( var csvReader = new CsvReader(reader))
            {
                var fileData = csvReader.GetRecords<TravelTimeFileRow>();
                var travelTimes = Map(fileData, direction);

                return travelTimes;
            }
        }

        private static List<TravelTime> Map(IEnumerable<TravelTimeFileRow> fileRows, Direction direction)
        {
            var travelTimes = fileRows
                .Select(r => Map(direction, r))
                .ToList();

            var updatedTravelTimes = MapEndDate(travelTimes);

            return updatedTravelTimes;
        }

        private static TravelTime Map(Direction direction, TravelTimeFileRow r)
        {
            return new TravelTime
            {
                Direction = direction,
                Start = DateTime.Parse(r.Date_Time),
                End = DateTime.Today,
                Time = Double.Parse(r.DirectionTravelTimeMinutes)
            };
        }

        private static List<TravelTime> MapEndDate(List<TravelTime> travelTimes)
        {
            var travelTimesSorted = travelTimes.OrderBy(t => t.Start).ToList();
            var countOfTravelTimes = travelTimesSorted.Count;

            for (var index = 0; index < countOfTravelTimes; index++)
            {
                var nextIndex = index + 1;

                if (nextIndex < countOfTravelTimes)
                {
                    var travelTime = travelTimesSorted[index];
                    var nextTime = travelTimesSorted[nextIndex];

                    travelTime.End = nextTime.Start;
                }

            }

            return travelTimesSorted;
        }

      
    }
}
