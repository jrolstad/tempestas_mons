using System.Collections.Generic;

namespace tempestas_mons.web.Models.api
{
    public class RoadConditionApiModel
    {
        public string StartDate { get; set; }

        public string StartDateYear { get; set; }
        public string StartDateMonth{ get; set; }
        public string StartDateDay { get; set; }
        public string StartDateDayOfWeek { get; set; }

        public string EndDate { get; set; }

        public string EndDateYear { get; set; }
        public string EndDateMonth { get; set; }
        public string EndDateDay { get; set; }
        public string EndDateDayOfWeek { get; set; }

        public double? Temperature { get; set; }

        public string Weather { get; set; }

        public string RoadConditionText { get; set; }

        public List<TravelRestrictionApiModel> TravelRestrictions { get; set; }
    }

    public class TravelRestrictionApiModel
    {
        public string Direction { get; set; }

        public List<string> Restrictions { get; set; }

        public string RestrictionMessage { get; set; }
    }
}