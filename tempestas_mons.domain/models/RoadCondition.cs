using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tempestas_mons.domain.models
{
    public class RoadCondition
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public double Temperature { get; set; }

        public string Weather { get; set; }

        public string RoadConditionText { get; set; }

        public List<TravelRestriction> TravelRestrictions { get; set; }
    }

    public class TravelRestriction
    {
        public Direction Direction {get;set;}

        public List<Restriction> Restrictions {get;set;}

        public string RestrictionMessage {get;set;}
    }

    public enum Restriction
    {
        NoRestrictions,
        TractionTiresAdvised,
        TractionTiresRequired,
        ChainsRequiredVehiclesOver10000GVW,
        ChainsRequired,
    }
}
