using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using tempestas_mons.domain.models;
using tempestas_mons.domain.models.rawdata;

namespace tempestas_mons.domain.services
{
    public class RoadConditionRepository
    {
        private readonly StreamReaderFactory _streamReaderFactory;

        public RoadConditionRepository(StreamReaderFactory streamReaderFactory)
        {
            _streamReaderFactory = streamReaderFactory;
        }

        public List<RoadCondition> Get()
        {
            const string fileName = "tempestas_mons.domain.data.csv.RoadConditions_I-90.csv";
            using (var reader = _streamReaderFactory.GetEmbeddedFile(fileName))
            using (var csvReader = new CsvReader(reader))
            {
                var fileData = csvReader.GetRecords<RoadConditionFileRow>();
                var travelTimes = Map(fileData);

                return travelTimes;
            }
        }

        private List<RoadCondition> Map(IEnumerable<RoadConditionFileRow> fileData)
        {
            var roadConditions = fileData
                .Select(Map)
                .ToList();

            var updatedRoadConditions = MapEndDate(roadConditions);

            return updatedRoadConditions;
        }

        private RoadCondition Map(RoadConditionFileRow fileRow)
        {
            var travelRestrictions = MapTravelRestrictions(fileRow);

            double? temperature = null;
            if(!string.IsNullOrWhiteSpace(fileRow.Temperature))
                temperature = double.Parse(fileRow.Temperature);
            
            return new RoadCondition
            {
                Start = DateTime.Parse(fileRow.DateUpdated),
                End = DateTime.Today,
                RoadConditionText = fileRow.RoadCondition,
                Temperature = temperature,
                Weather = fileRow.Weather,
                TravelRestrictions = travelRestrictions
            };
        }

        private List<TravelRestriction> MapTravelRestrictions(RoadConditionFileRow fileRow)
        {
            var travelRestrictions = new List<TravelRestriction>();

            var eastboundRestriction = MapTravelRestriction(fileRow.RestrictionText1, Direction.Eastbound);
            var westboundRestriction = MapTravelRestriction(fileRow.RestrictionText2, Direction.Westbound);
            travelRestrictions.Add(eastboundRestriction);
            travelRestrictions.Add(westboundRestriction);

            return travelRestrictions;
        }

        private TravelRestriction MapTravelRestriction(string restrictionText, Direction direction)
        {
            var restrictions = MapRestrictions(restrictionText);

            return new TravelRestriction
            {
                Direction = direction,
                RestrictionMessage = restrictionText,
                Restrictions = restrictions
            };
        }

        private List<Restriction> MapRestrictions(string restrictionText)
        {
            var restrictions = new List<Restriction>();

            if (restrictionText.Contains("Vehicles Over 10,000 GVW Chains Required"))
                restrictions.Add(Restriction.ChainsRequiredVehiclesOver10000GVW);

            if (restrictionText.Contains("Chains Required All Vehicles Including All Wheel Drive"))
                restrictions.Add(Restriction.ChainsRequiredAllVehicles);

            if (restrictionText.Contains("Chains Required All Vehicles/ Except All-Wheel Drive"))
                restrictions.Add(Restriction.ChainsRequiredExceptAllWheelDrive);

            if (restrictionText.Contains("No Restrictions"))
                restrictions.Add(Restriction.NoRestrictions);

            if (restrictionText.Contains("Pass Closed"))
                restrictions.Add(Restriction.PassClosed);

            if (restrictionText.Contains("Temporarily Closed"))
                restrictions.Add(Restriction.PassClosed);

            if (restrictionText.Contains("CLOSED FOR THE SEASON"))
                restrictions.Add(Restriction.PassClosed);

            if (restrictionText.Contains("Traction Advisory"))
                restrictions.Add(Restriction.TractionTiresAdvised);

            if (restrictionText.Contains("Traction Tires Required"))
                restrictions.Add(Restriction.TractionTiresRequired);

            if (restrictionText.Contains("Traffic Delayed"))
                restrictions.Add(Restriction.TrafficDelayed);

            if (restrictionText.Contains("Traffic Delayed for Avalanche Control"))
                restrictions.Add(Restriction.TrafficDelayedForAvalancheControl);

            if (restrictionText.Contains("Traffic stopped for avalanche control"))
                restrictions.Add(Restriction.TrafficStoppedForAvalancheControl);

            if (restrictionText.Contains("Oversize Vehicles Prohibited"))
                restrictions.Add(Restriction.OversizeVehiclesProhibited);

            return restrictions;
        }

        private static List<RoadCondition> MapEndDate(List<RoadCondition> roadConditions)
        {
            var travelTimesSorted = roadConditions.OrderBy(t => t.Start).ToList();
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
